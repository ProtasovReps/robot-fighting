using FiniteStateMachine;
using Reflex.Attributes;

namespace MovementSystem
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