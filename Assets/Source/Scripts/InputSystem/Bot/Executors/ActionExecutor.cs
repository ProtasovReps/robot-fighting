using System;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace InputSystem.Bot.Executor
{
    public abstract class ActionExecutor<TTargetState> : IDisposable
        where TTargetState : State
    {
        private const float SubscriptionDelay = 0.5f;

        private readonly CompositeDisposable _subscriptions;

        private IDisposable _currentSubscription;
        private BotAction _currentAction;

        protected ActionExecutor(IStateMachine stateMachine)
        {
            _subscriptions = new CompositeDisposable(2);
            Type targetState = typeof(TTargetState);

            stateMachine.Value
                .DelaySubscription(TimeSpan.FromSeconds(SubscriptionDelay))
                .Where(state => state.Type == targetState)
                .Subscribe(_ => Activate())
                .AddTo(_subscriptions);

            stateMachine.Value
                .Pairwise()
                .Where(pair => pair.Previous.Type == targetState)
                .Where(pair => pair.Current.Type != targetState)
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