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
            Animator animator,
            IStateMachine stateMachine,
            FighterParameters fighterParameters,
            PositionTranslation positionTranslation)
        {
            var animations = new CharacterAnimation[]
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
                new TriggerAnimation<DownDeathState>(stateMachine, animator, MotionHashes.DownDeath)
            };

            new AnimationDurationChanger(animator, stateMachine, fighterParameters);
            new MoveAnimationSpeed(animator, positionTranslation,fighterParameters.MoveSpeed);
            
            animatedCharacter.Initialize(animations);
        }
    }
}