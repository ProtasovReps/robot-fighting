using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace EffectSystem.StateDependent
{
    public abstract class StateDependentEffect<TState> : MonoBehaviour
    where TState : State
    {
        [Inject]
        private void Inject(BotStateMachine botStateMachine, PlayerStateMachine playerStateMachine)
        {
            Observable<State> botHitted = botStateMachine.Value
                .Where(value => value is TState); 
            
            Observable<State> playerHitted = playerStateMachine.Value
                .Where(value => value is TState);

            Observable.Merge(botHitted, playerHitted)
                .Subscribe(_ => Apply())
                .AddTo(this);
        }

        protected abstract void Apply();
    }
}