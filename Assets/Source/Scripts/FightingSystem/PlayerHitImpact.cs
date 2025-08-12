using FiniteStateMachine;
using Reflex.Attributes;

namespace FightingSystem
{
    public class PlayerHitImpact : HitImpact
    {
        [Inject]
        private void Inject(PlayerStateMachine stateMachine)
        {
            Initialize(stateMachine);
        }
    }
}