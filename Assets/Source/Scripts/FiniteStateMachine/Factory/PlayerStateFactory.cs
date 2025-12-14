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
                new UpHittedState(),
                new DownHittedState(),
                new JumpState(),
                new MoveJumpState(),
                new UpAttackState(),
                new DownAttackState(),
                new BlockState(),
                new SuperAttackState(),
                new UpDeathState(),
                new DownDeathState()
            };
        }
    }
}