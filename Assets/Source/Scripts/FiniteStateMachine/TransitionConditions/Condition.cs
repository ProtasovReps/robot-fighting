using R3;

namespace FiniteStateMachine.TransitionConditions
{
    public abstract class Condition<T>
    {
        public abstract Observable<T> GetCondition();
    }
}