using System;
using Interface;
using R3;

namespace FiniteStateMachine
{
    public class TransitionInitializer : IDisposable
    {
        private readonly CharacterStateMachine _machine;
        private readonly CompositeDisposable _disposables;

        public TransitionInitializer(CharacterStateMachine stateMachine)
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
            where TTargetState : IState
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