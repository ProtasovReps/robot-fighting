using System;
using Interface;
using R3;

namespace FighterStateSystem
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
            Action observer = null)
            where TTargetState : IState
        {
            var transition = new Transition<TTargetState>(_machine);
            
            observable.Subscribe(_ =>
            {
                transition.Transit();
                observer?.Invoke();
            })
            .AddTo(_disposables);
            
            return this;
        }
    }
}