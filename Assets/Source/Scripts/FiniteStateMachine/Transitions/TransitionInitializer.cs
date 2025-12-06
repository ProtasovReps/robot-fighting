using System;
using FiniteStateMachine.States;
using FiniteStateMachine.Transitions.Factory;
using R3;

namespace FiniteStateMachine.Transitions
{
    public class TransitionInitializer : IDisposable
    {
        private readonly TransitionFactory _transitionFactory;
        private readonly CompositeDisposable _disposables;
        private readonly StateMachine _stateMachine;
        
        public TransitionInitializer(TransitionFactory transitionFactory, StateMachine stateMachine)
        {
            if (transitionFactory == null)
            {
                throw new ArgumentNullException(nameof(transitionFactory));
            }

            if (stateMachine == null)
            {
                throw new ArgumentNullException(nameof(stateMachine));
            }
            
            _transitionFactory = transitionFactory;
            _stateMachine = stateMachine;
            
            _disposables = new CompositeDisposable();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }

        public TransitionInitializer InitializeTransition<TTargetState, T>(
            Observable<T> observable,
            Func<Unit, bool> condition)
            where TTargetState : State
        {
            var transition = _transitionFactory.Produce<TTargetState>(_stateMachine);

            observable
                .Select(_ => Unit.Default)
                .Where(condition)
                .Subscribe(_ => transition.Transit())
                .AddTo(_disposables);

            return this;
        }
    }
}