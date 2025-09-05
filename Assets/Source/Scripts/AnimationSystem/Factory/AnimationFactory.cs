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
                new TriggerAnimation<IdleState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<JumpState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Jump),
                new TriggerAnimation<MoveLeftState>(stateMachine, animatedCharacter.Animator, AnimationHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(stateMachine, animatedCharacter.Animator, AnimationHashes.MoveRight),
                new TriggerAnimation<UpAttackState>(stateMachine, animatedCharacter.Animator, AnimationHashes.ArmAttack),
                new TriggerAnimation<DownAttackState>(stateMachine, animatedCharacter.Animator, AnimationHashes.LegAttack),
                new TriggerAnimation<UpHittedState>(stateMachine, animatedCharacter.Animator, AnimationHashes.HeadHit),
                new TriggerAnimation<DownHittedState>(stateMachine, animatedCharacter.Animator, AnimationHashes.LegsHit),
                new TriggerAnimation<BlockState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Block),
                new TriggerAnimation<SpecialAttackState>(stateMachine, animatedCharacter.Animator, AnimationHashes.Special)
            };

            new AnimationDurationChanger(animatedCharacter.Animator, stateMachine, fighterData);
            new MoveAnimationSpeed(animatedCharacter.Animator,positionTranslation,fighterData.MoveSpeed);
            
            animatedCharacter.Initialize(animations);
        }
    }
}