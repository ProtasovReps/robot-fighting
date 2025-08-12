using FiniteStateMachine;
using Reflex.Attributes;

namespace FightingSystem
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