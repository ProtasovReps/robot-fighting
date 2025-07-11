using Interface;
using R3;
using UnityEngine;

namespace InputSystem
{
    public class BotInputReader : MonoBehaviour, IDirectionChangeable
    {
        private BotMovementInput _botMovementInput;
        private ReactiveProperty<float> _direction;

        public ReadOnlyReactiveProperty<float> Direction => _direction;

        private void Awake()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_botMovementInput.Direction))
                .AddTo(this);
        }

        public void Initialize(BotMovementInput botMovementInput)
        {
            _botMovementInput = botMovementInput;
            _direction = new ReactiveProperty<float>();
        }
    }
}