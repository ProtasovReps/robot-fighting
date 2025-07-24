using AnimationSystem.Factory;
using CharacterSystem.Data;
using Interface;

namespace CharacterSystem.Factory
{
    public class BotFactory : FighterFactory
    {
        public BotData Produce(BotData playerData, AnimationFactory animationFactory, IBotStateMachine stateMachine,
            IConditionAddable conditionAddable)
        {
            return base.Produce(playerData, animationFactory, stateMachine, conditionAddable) as BotData;
        }
    }
}