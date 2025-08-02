using R3;

namespace Interface
{
    public interface IExecutable
    {
        Observable<Unit> Executed { get; }
    }
}