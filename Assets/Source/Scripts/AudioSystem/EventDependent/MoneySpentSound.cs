using Interface;
using R3;

namespace AudioSystem.EventDependent
{
    public class MoneySpentSound : EventSound
    {
        private Observable<Unit> _observable;
        
        public void Initialize(IMoneySpendable moneyAddable)
        {
            _observable = moneyAddable.Spent;
        }
        
        protected override Observable<Unit> GetObservable()
        {
            return _observable;
        }
    }
}