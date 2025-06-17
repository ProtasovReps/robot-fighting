using R3;

namespace Interface
{
    public interface IStateExecutor
    {
        Subject<Unit> IsExecuted { get; }
    }
}