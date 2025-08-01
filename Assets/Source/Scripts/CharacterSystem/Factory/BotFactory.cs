using AnimationSystem.Factory;
using CharacterSystem.Data;
using FiniteStateMachine.Conditions;
using Interface;

namespace CharacterSystem.Factory
{
    public class BotFactory : FighterFactory
    {
        public BotData Produce(AnimationFactory animationFactory, IStateMachine stateMachine,
            BotConditionBuilder conditionAddable)
        {
            return base.Produce(animationFactory, stateMachine, conditionAddable) as BotData;
        }
    }
}