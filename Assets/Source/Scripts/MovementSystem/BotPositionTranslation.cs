using Interface;
using Reflex.Attributes;

namespace MovementSystem
{
    public class BotPositionTranslation : PositionTranslation
    {
        [Inject]
        private void Inject(IBotStateMachine stateMachine, IBotConditionAddable conditionAddable)
        {
            SetStateMachine(stateMachine, conditionAddable);
        }
    }
}