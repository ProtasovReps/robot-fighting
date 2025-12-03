using System;
using Extensions;
using Interface;
using R3;

namespace InputSystem.Bot
{
    public class BotMoveInput : IMoveInput, IDisposable
    {
        private readonly ReactiveProperty<float> _direction = new();
        private readonly IDisposable _subscription;

        public BotMoveInput()
        {
            _subscription = Observable.EveryUpdate()
                .Subscribe(_ => _direction.OnNext(_direction.CurrentValue));
        }
        
        public ReadOnlyReactiveProperty<float> Value => _direction;
        
        public void Dispose()
        {
            _subscription.Dispose();
        }
        
        public void MoveLeft()
        {
            _direction.OnNext(Directions.Left);
        }

        public void MoveRight()
        {
            _direction.OnNext(Directions.Right);
        }

        public void Stop()
        {
            _direction.OnNext(Directions.InPlace);
        }
    }
}