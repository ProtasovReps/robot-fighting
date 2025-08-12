using FiniteStateMachine;
using FiniteStateMachine.Conditions;

namespace MovementSystem
{
    public class PlayerMovement : Movement<PlayerStateMachine, PlayerConditionBuilder>
    {
    }
}