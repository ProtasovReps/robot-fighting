using AnimationSystem.Factory;
using CharacterSystem.Data;
using Interface;

namespace CharacterSystem.Factory
{
    public class BotFactory : FighterFactory
    {
        public BotData Produce(AnimationFactory animationFactory, IBotStateMachine stateMachine,
            IConditionAddable conditionAddable)
        {
            return base.Produce(animationFactory, stateMachine, conditionAddable) as BotData;
        }
    }
}