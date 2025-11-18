using Interface;
using R3;

namespace AudioSystem.EventDependent
{
    public class NotEnoughMoneySound : EventSound
    {
        private Observable<Unit> _observable;
        
        public void Initialize(IMoneySpendable moneySpendable)
        {
            _observable = moneySpendable.FailedSpend;
        }
        
        protected override Observable<Unit> GetObservable()
        {
            return _observable;
        }
    }
}