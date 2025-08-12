using AnimationSystem.Factory;
using CharacterSystem.Data;
using FiniteStateMachine.Conditions;
using Interface;

namespace CharacterSystem.Factory
{
    public class PlayerFactory : FighterFactory
    {
        public PlayerData Produce(AnimationFactory animationFactory,
            IStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            return base.Produce(animationFactory, stateMachine, conditionAddable) as PlayerData;
        }
    }
}