using R3;
using UnityEngine;

namespace InputSystem
{
    public class InputReader : MonoBehaviour
    {
        private UserInput _input;
        private Subject<Unit> _jumpPressed;
        private Subject<Unit> _upAttackPressed;
        private ReactiveProperty<float> _direction;
        
        public Observable<Unit> JumpPressed => _jumpPressed;
        public Observable<Unit> UpAttackPressed => _upAttackPressed;
        public ReadOnlyReactiveProperty<float> Direction => _direction;
        
        private void Awake()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_input.Player.Move.ReadValue<float>()))
                .AddTo(this);
            
            _input.Player.Jump.performed += _ => _jumpPressed.OnNext(Unit.Default);
            _input.Player.FistPunch.performed += _ => _upAttackPressed.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _input.Disable();
        }

        public void Initialize(UserInput input)
        {
            _input = input;
            _jumpPressed = new Subject<Unit>();
            _upAttackPressed  = new Subject<Unit>();
            _direction = new ReactiveProperty<float>();
            _input.Enable();
        }
    }
}