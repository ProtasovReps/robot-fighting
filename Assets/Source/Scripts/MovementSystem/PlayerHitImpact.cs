using Interface;
using Reflex.Attributes;

namespace MovementSystem
{
    public class PlayerHitImpact : HitImpact
    {
        [Inject]
        private void Inject(IPlayerStateMachine stateMachine)
        {
            Initialize(stateMachine);
        }
    }
}