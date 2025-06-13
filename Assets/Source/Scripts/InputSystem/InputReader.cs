using R3;
using UnityEngine;

namespace InputSystem
{
    public class InputReader : MonoBehaviour
    {
        private UserInput _input;
        private Subject<Unit> _jumpPressed;
        private ReactiveProperty<float> _direction;

        public Observable<Unit> JumpPressed => _jumpPressed;
        public ReadOnlyReactiveProperty<float> Direction => _direction;
        
        private void Awake()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_input.Player.Move.ReadValue<float>()))
                .AddTo(this);
            
            _input.Player.Jump.performed += inputAction => _jumpPressed.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _jumpPressed.OnCompleted();
            _direction.OnCompleted();
            _input.Disable();
        }

        public void Initialize(UserInput input)
        {
            _input = input;
            _jumpPressed = new Subject<Unit>();
            _direction = new ReactiveProperty<float>();
            
            _input.Enable();
        }
    }
}