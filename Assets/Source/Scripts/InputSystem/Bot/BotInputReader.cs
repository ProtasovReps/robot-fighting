using Interface;
using R3;
using UnityEngine;

namespace InputSystem
{
    public class BotInputReader : MonoBehaviour, IDirectionChangeable
    {
        private BotMovementInput _botMovementInput;
        private BotAttackInput _botAttackInput;
        private ReactiveProperty<float> _direction;

        public ReadOnlyReactiveProperty<float> Direction => _direction;
        public Observable<Unit> UpAttackPressed => _botAttackInput.UpAttack;
        public Observable<Unit> DownAttackPressed => _botAttackInput.DownAttack;
        
        private void Awake()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_botMovementInput.Direction))
                .AddTo(this);
        }

        public void Initialize(BotMovementInput botMovementInput, BotAttackInput botAttackInput)
        {
            _botMovementInput = botMovementInput;
            _botAttackInput = botAttackInput;
            _direction = new ReactiveProperty<float>();
        }
    }
}