using R3;

namespace FiniteStateMachine.Conditions
{
    public abstract class Condition
    {
        public abstract Observable<Unit> GetCondition();
    }
}