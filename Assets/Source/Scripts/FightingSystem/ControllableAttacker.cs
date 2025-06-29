using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;

namespace FightingSystem
{
    public class ControllableAttacker : Attacker
    {
        private IStateChangeable _stateMachine;

        [Inject]
        private void Inject(IStateChangeable stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.CurrentState
                .Where(state => state is AttackState)
                .Subscribe(state => Attack(state.Type))
                .AddTo(this);
        }
    }
}