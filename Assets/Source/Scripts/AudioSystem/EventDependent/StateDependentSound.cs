using FiniteStateMachine;
using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;

namespace AudioSystem.EventDependent
{
    public class StateDependentSound<TState> : EventSound
        where TState : State
    {
        private IValueChangeable<State> _playerStateMachine;
        private IValueChangeable<State> _botStateMachine;
        
        [Inject]
        private void Inject(PlayerStateMachine playerStateMachine, BotStateMachine botStateMachine)
        {
            _playerStateMachine = playerStateMachine;
            _botStateMachine = botStateMachine;
        }

        protected override Observable<Unit> GetObservable()
        {
            Observable<Unit> playerObservable = CreateObservable(_playerStateMachine);
            Observable<Unit> botObservable = CreateObservable(_botStateMachine);

            return Observable.Merge(playerObservable, botObservable);
        }

        private Observable<Unit> CreateObservable(IValueChangeable<State> valueChangeable)
        {
            return valueChangeable.Value
                .Where(state => state.Type == typeof(TState))
                .Select(_ => Unit.Default);
        }
    }
}