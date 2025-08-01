using FiniteStateMachine;
using Reflex.Attributes;

namespace MovementSystem
{
    public class BotHitImpact : HitImpact
    {
        [Inject]
        private void Inject(BotStateMachine botStateMachine)
        {
            Initialize(botStateMachine);
        }
    }
}