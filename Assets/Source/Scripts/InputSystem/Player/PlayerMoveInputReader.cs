using System;
using Interface;
using R3;

namespace InputSystem
{
    public class PlayerMoveInputReader : IMoveInput, IDisposable 
    {
        private readonly ReactiveProperty<int> _direction;
        private readonly Subject<Unit> _jumpPressed;
        private readonly IDisposable _subscription;
       
        public PlayerMoveInputReader(UserInput userInput)
        {
            _jumpPressed = new Subject<Unit>();
            _direction = new ReactiveProperty<int>();
            
            userInput.Player.Jump.performed += _ => _jumpPressed.OnNext(Unit.Default);
            
            _subscription = Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext((int)userInput.Player.Move.ReadValue<float>()));
        }
        
        public ReadOnlyReactiveProperty<int> Value => _direction;
        public Observable<Unit> JumpPressed => _jumpPressed;
      
        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}