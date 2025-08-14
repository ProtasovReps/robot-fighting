using CharacterSystem.Data;
using FiniteStateMachine.Conditions;
using Interface;

namespace CharacterSystem.Factory
{
    public class PlayerFactory : FighterFactory
    {
        public PlayerData Produce(IStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            return base.Produce(stateMachine, conditionAddable) as PlayerData;
        }
    }
}