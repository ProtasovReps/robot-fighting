using CharacterSystem.Data;
using FiniteStateMachine.Conditions;
using Interface;

namespace CharacterSystem.Factory
{
    public class BotFactory : FighterFactory
    {
        public BotData Produce(IStateMachine stateMachine, BotConditionBuilder conditionAddable)
        {
            return base.Produce(stateMachine, conditionAddable) as BotData;
        }
    }
}