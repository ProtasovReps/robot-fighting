using R3;

namespace Interface
{
    public interface IMoneySpendable
    {
        Observable<Unit> FailedSpend { get; } 

        bool TrySpend(int amount);
    }
}