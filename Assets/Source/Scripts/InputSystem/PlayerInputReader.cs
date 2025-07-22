using Interface;
using R3;
using UnityEngine;

namespace InputSystem
{
    public class PlayerInputReader : MonoBehaviour, IDirectionChangeable
    {
        private UserInput _input;
        private Subject<Unit> _jumpPressed;
        private Subject<Unit> _punchPressed;
        private Subject<Unit> _kickPressed;
        private Subject<Unit> _blockPressed;
        private ReactiveProperty<float> _direction;

        public Observable<Unit> JumpPressed => _jumpPressed;
        public Observable<Unit> PunchPressed => _punchPressed;
        public Observable<Unit> KickPressed => _kickPressed;
        public Observable<Unit> BlockPressed => _blockPressed;
        
        public ReadOnlyReactiveProperty<float> Direction => _direction;

        private void Awake()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_input.Player.Move.ReadValue<float>()))
                .AddTo(this);

            _input.Player.Jump.performed += _ => _jumpPressed.OnNext(Unit.Default);
            _input.Player.Punch.performed += _ => _punchPressed.OnNext(Unit.Default);
            _input.Player.Kick.performed += _ => _kickPressed.OnNext(Unit.Default);
            _input.Player.Block.performed += _ => _blockPressed.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            _input.Disable();
        }

        public void Initialize(UserInput input)
        {
            _input = input;
            _jumpPressed = new Subject<Unit>();
            _punchPressed = new Subject<Unit>();
            _kickPressed = new Subject<Unit>();
            _blockPressed = new Subject<Unit>();
            _direction = new ReactiveProperty<float>();
            _input.Enable();
        }
    }
}