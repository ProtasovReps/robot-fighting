using CharacterSystem.Parameters;
using Extensions;
using FiniteStateMachine.States;
using Interface;
using MovementSystem;
using UnityEngine;

namespace AnimationSystem.Factory
{
    public class AnimationFactory
    {
        public void Produce(
            AnimatedCharacter animatedCharacter,
            IStateMachine stateMachine,
            FighterParameters fighterParameters,
            PositionTranslation positionTranslation,
            Disposer disposer,
            float moveSpeed)
        {
            Animator animator = animatedCharacter.Animator;
            
            var animations = new IAnimation[]
            {
                new TriggerAnimation<IdleState>(stateMachine, animator, MotionHashes.Idle),
                new TriggerAnimation<JumpState>(stateMachine, animator, MotionHashes.Jump),
                new TriggerAnimation<MoveLeftState>(stateMachine, animator, MotionHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(stateMachine, animator, MotionHashes.MoveRight),
                new TriggerAnimation<UpAttackState>(stateMachine, animator, MotionHashes.ArmAttack),
                new TriggerAnimation<DownAttackState>(stateMachine, animator, MotionHashes.LegAttack),
                new TriggerAnimation<SuperAttackState>(stateMachine, animator, MotionHashes.Super),
                new TriggerAnimation<UpHittedState>(stateMachine, animator, MotionHashes.HeadHit),
                new TriggerAnimation<DownHittedState>(stateMachine, animator, MotionHashes.LegsHit),
                new TriggerAnimation<BlockState>(stateMachine, animator, MotionHashes.Block),
                new TriggerAnimation<SpecialAttackState>(stateMachine, animator, MotionHashes.Special),
                new TriggerAnimation<StretchState>(stateMachine, animator, MotionHashes.Stretch),
                new TriggerAnimation<UpDeathState>(stateMachine, animator, MotionHashes.UpDeath),
                new TriggerAnimation<DownDeathState>(stateMachine, animator, MotionHashes.DownDeath),
            };

            var durationChanger = new AnimationDurationChanger(animator, stateMachine, fighterParameters);
            var animationSpeed = new MoveAnimationSpeed(animator, positionTranslation, moveSpeed);

            for (int i = 0; i < animations.Length; i++)
            {
                disposer.Add(animations[i]);
            }
            
            disposer.Add(durationChanger);
            disposer.Add(animationSpeed);
            
            animatedCharacter.Initialize(animations);
        }
    }
}