using R3;

namespace FiniteStateMachine.Conditions
{
    public class SourceCondition<T> : Condition
    {
        private readonly Observable<T> _source;

        public SourceCondition(Observable<T> source)
        {
            _source = source;
        }

        public override Observable<Unit> GetCondition()
        {
            return _source.Select(_ => Unit.Default);
        }
    }
}