using Interface;
using R3;
using Reflex.Attributes;

namespace AudioSystem.EventDependent
{
    public class MoneySpentSound : EventSound
    {
        private Observable<Unit> _observable;

        [Inject]
        private void Inject(IMoneySpendable moneySpendable)
        {
            _observable = moneySpendable.Spent;
        }
        
        protected override Observable<Unit> GetObservable()
        {
            return _observable;
        }
    }
}