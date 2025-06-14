using System.Threading;
using Cysharp.Threading.Tasks;
using InputSystem;
using Interface;
using Reflex.Attributes;
using UnityEngine;
using R3;

namespace FightingSystem
{
    public class Attacker : MonoBehaviour
    {
        private IAttack _attack;
        private InputReader _inputReader;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Inject(InputReader inputReader)
        {
            _inputReader = inputReader;
        }
        
        private void Start()
        {
            _inputReader.UpAttackPressed
                .Subscribe(_ => Attack().Forget()) // А если в кд? СТЕЙТ МАШИНА!
                .AddTo(this);
        }

        public void Initialize(IAttack attack)
        {
            _attack = attack;
        }

        private async UniTaskVoid Attack() //или сюда проверку впиндюрить на стейт
        {
            _cancellationTokenSource = new CancellationTokenSource();
            // аниматор проиграть анимацию
            await UniTask.WaitForSeconds(_attack.Delay, false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            // если отменили атаку - урон не нанесется
        }
    }
}
