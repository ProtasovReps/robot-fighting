using AnimationSystem.Factory;
using FiniteStateMachine.Conditions;
using Interface;

namespace CharacterSystem.Factory
{
    public class PlayerFactory : FighterFactory
    {
        public void Produce(AnimationFactory animationFactory,
            IStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            base.Produce(animationFactory, stateMachine, conditionAddable);
        }
    }
}