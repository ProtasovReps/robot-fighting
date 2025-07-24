using AnimationSystem.Factory;
using CharacterSystem.Data;
using Interface;

namespace CharacterSystem.Factory
{
    public class PlayerFactory : FighterFactory
    {
        public PlayerData Produce(PlayerData playerData, AnimationFactory animationFactory,
            IPlayerStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            return base.Produce(playerData, animationFactory, stateMachine, conditionAddable) as PlayerData;
        }
    }
}