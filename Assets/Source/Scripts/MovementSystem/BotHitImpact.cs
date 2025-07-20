using Interface;
using Reflex.Attributes;

namespace MovementSystem
{
    public class BotHitImpact : HitImpact
    {
        [Inject]
        private void Inject(IBotStateMachine botStateMachine)
        {
            Initialize(botStateMachine);
        }
    }
}