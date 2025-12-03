using System;
using Interface;
using R3;

namespace InputSystem
{
    public class PlayerMoveInputReader : IMoveInput, IDisposable 
    {
        private readonly ReactiveProperty<float> _direction;
        private readonly IDisposable _subscription;
       
        public PlayerMoveInputReader(UserInput userInput)
        {
            var jumpPressed = new Subject<Unit>();
            
            _direction = new ReactiveProperty<float>();
            JumpPressed = jumpPressed;
            
            userInput.Player.Jump.performed += _ => jumpPressed.OnNext(Unit.Default);
            
            _subscription = Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext((int)userInput.Player.Move.ReadValue<float>()));
        }
        
        public ReadOnlyReactiveProperty<float> Value => _direction;
        public Observable<Unit> JumpPressed { get; }
    
        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}