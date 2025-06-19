using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace FightingSystem
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private Spherecaster _spherecaster;
        
        private IAttack _attack;
        private CancellationTokenSource _cancellationTokenSource;
        private IStateChangeable _stateMachine;
        
        [Inject]
        private void Inject(IStateChangeable stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.CurrentState //здесь нужен будет кулдаун, 
                .Where(state => state.Type == typeof(ArmAttackState)) // у наследников менять необходимый тип
                .Subscribe(_ => AttackDelayed().Forget())
                .AddTo(this);
        }
        
        public void Initialize(IAttack attack)
        {
            _attack = attack;
        }
        
        private async UniTaskVoid AttackDelayed() // у наследников менять тип атаки
        {
            Debug.Log("Бью");
            _cancellationTokenSource = new CancellationTokenSource();
            // аниматор проиграть анимацию
            await UniTask.WaitForSeconds(_attack.Delay, false, PlayerLoopTiming.Update, _cancellationTokenSource.Token, true);
            // если отменили атаку - урон не нанесется

            if (_spherecaster.TryFindDamageable(out IDamageable damageable) == false)
                return;
            
            _attack.ApplyDamage(damageable);
        }
    }
}
