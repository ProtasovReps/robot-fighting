using System;
using Interface;
using R3;

namespace InputSystem.Bot
{
    public abstract class BotInput : IDisposable
    {
        private readonly CompositeDisposable _subscriptions;
        
        private IDisposable _currentSubscription;
        private BotAction _currentAction;
        
        protected BotInput(IStateMachine stateMachine, Type targetState)
        {
            _subscriptions = new CompositeDisposable(2);
            
            stateMachine.CurrentState
                .DelaySubscription(TimeSpan.FromSeconds(0.5f)) 
                .Where(state => state.Type == targetState)
                .Subscribe(_ => Activate())
                .AddTo(_subscriptions);

            stateMachine.CurrentState
                .Pairwise()
                .Where(pair => pair.Previous.Type == targetState 
                               && pair.Current.Type != targetState)
                .Subscribe(_ => Deactivate())
                .AddTo(_subscriptions);
        }

        public virtual void Dispose()
        {
            _subscriptions.Dispose();
            Deactivate();
        }

        protected abstract BotAction GetAction();
        
        private void Activate()
        {
            _currentAction = GetAction();
            _currentSubscription = _currentAction.Executed
                .Subscribe(_ => Reactivate());
            
            _currentAction.Activate();
        }

        private void Reactivate()
        {
            Deactivate();
            Activate();
        }
        
        private void Deactivate()
        {
            _currentSubscription?.Dispose();
            _currentAction?.Deactivate();
        }
    }
}