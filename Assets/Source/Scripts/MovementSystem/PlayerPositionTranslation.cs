using Interface;
using Reflex.Attributes;

namespace MovementSystem
{
    public class PlayerPositionTranslation : PositionTranslation
    {
        [Inject]
        private void Inject(IPlayerStateMachine stateMachine, IPlayerConditionAddable conditionAddable)
        {
            SetStateMachine(stateMachine, conditionAddable);
        }
    }
}