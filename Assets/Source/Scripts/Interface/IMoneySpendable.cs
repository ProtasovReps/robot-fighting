using R3;

namespace Interface
{
    public interface IMoneySpendable
    {
        Observable<Unit> Spent { get; }
        Observable<Unit> FailedSpend { get; } 

        bool TrySpend(int amount);
    }
}