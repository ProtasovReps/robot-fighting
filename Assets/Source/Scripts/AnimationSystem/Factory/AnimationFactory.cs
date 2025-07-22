using Extensions;
using FiniteStateMachine.States;
using Interface;

namespace AnimationSystem.Factory
{
    public class AnimationFactory
    {
        public void Produce(AnimatedCharacter animatedCharacter, IStateMachine stateMachine)
        {
            var animations = new CharacterAnimation[]
            {
                new TriggerAnimation<IdleState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<JumpState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Jump),
                new TriggerAnimation<MoveLeftState>(stateMachine, animatedCharacter.Animator, AnimationHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(stateMachine, animatedCharacter.Animator, AnimationHashes.MoveRight),
                new TriggerAnimation<PunchState>(stateMachine, animatedCharacter.Animator, AnimationHashes.ArmAttack),
                new TriggerAnimation<KickState>(stateMachine, animatedCharacter.Animator, AnimationHashes.LegAttack),
                new TriggerAnimation<HittedState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Hit),
                new TriggerAnimation<BlockState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Block)
            };

            animatedCharacter.Initialize(animations);
        }
    }
}