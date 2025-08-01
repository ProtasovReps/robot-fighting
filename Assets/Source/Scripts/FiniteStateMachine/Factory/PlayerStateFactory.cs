using FiniteStateMachine.States;
using Interface;

namespace FiniteStateMachine.Factory
{
    public class PlayerStateFactory : StateFactory
    {
        protected override IState[] GetStates()
        {
            return new IState[]
            {
                new IdleState(),
                new MoveLeftState(),
                new MoveRightState(),
                new HittedState(),
                new JumpState(),
                new MoveJumpState(),
                new PunchState(),
                new KickState(),
                new BlockState()
            };
        }
    }
}