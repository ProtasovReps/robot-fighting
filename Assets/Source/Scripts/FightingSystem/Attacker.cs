using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using InputSystem;
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
        private InputReader _inputReader;
        private CancellationTokenSource _cancellationTokenSource;

        protected IAttack Attack => _attack;
        protected InputReader InputReader => _inputReader;
        
        [Inject]
        private void Inject(InputReader inputReader)
        {
            _inputReader = inputReader;
        }

        private void Start()
        {
            InputReader.UpAttackPressed // разные аттакеры будут разные клавиши обрабатывтаь?
                .Subscribe(_ => AttackDelayed().Forget()) // А если в кд? СТЕЙТ МАШИНА!
                .AddTo(this);
        }
        
        public void Initialize(IAttack attack)
        {
            _attack = attack;
        }
        
        private async UniTaskVoid AttackDelayed() //или сюда проверку впиндюрить на стейт
        {
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
