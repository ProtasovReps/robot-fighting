using FiniteStateMachine.States;

namespace FiniteStateMachine.Factory
{
    public class PlayerStateFactory : StateFactory
    {
        protected override State[] GetStates()
        {
            return new State[]
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