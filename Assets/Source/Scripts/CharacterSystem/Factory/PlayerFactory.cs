using AnimationSystem;
using CharacterSystem.Data;
using Extensions;
using FiniteStateMachine.States;
using Interface;

namespace CharacterSystem.Factory
{
    public class PlayerFactory : FighterFactory 
    {
        public PlayerFactory(FighterData data, IPlayerStateMachine machine) : base(data, machine)
        {
        }

        protected override CharacterAnimation[] GetAnimations()
        {
            return new CharacterAnimation[]
            {
                new TriggerAnimation<IdleState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<JumpState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.Jump),
                new TriggerAnimation<MoveLeftState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.MoveRight),
                new TriggerAnimation<PunchState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.ArmAttack),
                new TriggerAnimation<KickState>(StateMachine, FighterData.Fighter.Animator, AnimationHashes.LegAttack)
            };
        }
    }
}