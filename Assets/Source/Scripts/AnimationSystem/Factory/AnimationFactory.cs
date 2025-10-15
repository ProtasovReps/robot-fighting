using CharacterSystem.Data;
using Extensions;
using FiniteStateMachine.States;
using Interface;
using MovementSystem;

namespace AnimationSystem.Factory
{
    public class AnimationFactory
    {
        public void Produce(
            AnimatedCharacter animatedCharacter,
            IStateMachine stateMachine,
            FighterData fighterData,
            PositionTranslation positionTranslation)
        {
            var animations = new CharacterAnimation[]
            {
                new TriggerAnimation<IdleState>(stateMachine, animatedCharacter.Animator, MotionHashes.Idle),
                new TriggerAnimation<JumpState>(stateMachine, animatedCharacter.Animator, MotionHashes.Jump),
                new TriggerAnimation<MoveLeftState>(stateMachine, animatedCharacter.Animator, MotionHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(stateMachine, animatedCharacter.Animator, MotionHashes.MoveRight),
                new TriggerAnimation<UpAttackState>(stateMachine, animatedCharacter.Animator, MotionHashes.ArmAttack),
                new TriggerAnimation<DownAttackState>(stateMachine, animatedCharacter.Animator, MotionHashes.LegAttack),
                new TriggerAnimation<SuperAttackState>(stateMachine, animatedCharacter.Animator, MotionHashes.Super),
                new TriggerAnimation<UpHittedState>(stateMachine, animatedCharacter.Animator, MotionHashes.HeadHit),
                new TriggerAnimation<DownHittedState>(stateMachine, animatedCharacter.Animator, MotionHashes.LegsHit),
                new TriggerAnimation<BlockState>(stateMachine, animatedCharacter.Animator, MotionHashes.Block),
                new TriggerAnimation<SpecialAttackState>(stateMachine, animatedCharacter.Animator, MotionHashes.Special),
                new TriggerAnimation<StretchState>(stateMachine, animatedCharacter.Animator, MotionHashes.Stretch),
                new TriggerAnimation<UpDeathState>(stateMachine, animatedCharacter.Animator, MotionHashes.UpDeath),
                new TriggerAnimation<DownDeathState>(stateMachine, animatedCharacter.Animator, MotionHashes.DownDeath)
            };

            new AnimationDurationChanger(animatedCharacter.Animator, stateMachine, fighterData);
            new MoveAnimationSpeed(animatedCharacter.Animator,positionTranslation,fighterData.MoveSpeed);
            
            animatedCharacter.Initialize(animations);
        }
    }
}