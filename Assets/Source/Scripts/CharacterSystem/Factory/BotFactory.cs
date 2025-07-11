using AnimationSystem;
using CharacterSystem.Data;
using Extensions;
using FiniteStateMachine.States;
using Interface;

namespace CharacterSystem.Factory
{
    public class BotFactory : FighterFactory 
    {
        public BotFactory(FighterData data, IStateMachine stateMachine) : base(data, stateMachine)
        {
        }

        protected override CharacterAnimation[] GetAnimations()
        {
            return new CharacterAnimation[]
            {
                new TriggerAnimation<IdleState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<MoveLeftState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.MoveRight),
            };
        }
    }
}