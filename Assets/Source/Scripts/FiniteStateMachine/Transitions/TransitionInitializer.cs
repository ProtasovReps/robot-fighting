using System;
using FiniteStateMachine.States;
using R3;

namespace FiniteStateMachine.Transitions
{
    public class TransitionInitializer : IDisposable
    {
        private readonly StateMachine _machine;
        private readonly CompositeDisposable _disposables;

        public TransitionInitializer(StateMachine stateMachine)
        {
            if (stateMachine == null)
                throw new ArgumentNullException(nameof(stateMachine));

            _machine = stateMachine;
            _disposables = new CompositeDisposable();
        }

        public void Dispose() // диспозер нужен
        {
            _disposables?.Dispose();
        }

        public TransitionInitializer InitializeTransition<TTargetState, T>(
            Observable<T> observable,
            Func<Unit, bool> condition)
            where TTargetState : State
        {
            var transition = new Transition<TTargetState>(_machine);

            observable
                .Select(_ => Unit.Default)
                .Where(condition)
                .Subscribe(_ => transition.Transit())
                .AddTo(_disposables);

            return this;
        }
    }
}