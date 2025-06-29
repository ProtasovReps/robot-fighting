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
            _stateMachine.CurrentState //здесь нужен будет кулдаун, 
                .Where(state => state.Type == typeof(PunchState)) // у наследников менять необходимый тип
                .Subscribe(_ => Attack())
                .AddTo(this);
        }

    }
}