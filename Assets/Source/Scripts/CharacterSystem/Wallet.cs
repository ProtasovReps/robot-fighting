using System;
using Interface;
using R3;

namespace CharacterSystem
{
    public class Wallet : IValueChangeable<int>, IMoneySpendable, IMoneyAddable
    {
        private readonly ReactiveProperty<int> _value;
        private readonly Subject<Unit> _spent;
        private readonly Subject<Unit> _failedSpend;

        public Wallet(int startValue)
        {
            ValidateAmount(startValue);

            _value = new ReactiveProperty<int>(startValue);
            _spent = new Subject<Unit>();
            _failedSpend = new Subject<Unit>();
        }

        public ReadOnlyReactiveProperty<int> Value => _value;
        public Observable<Unit> Spent => _spent;
        public Observable<Unit> FailedSpend => _failedSpend;

        public void Add(int amount)
        {
            ValidateAmount(amount);

            _value.OnNext(_value.CurrentValue + amount);
        }

        public bool TrySpend(int amount)
        {
            ValidateAmount(amount);

            if (amount > _value.CurrentValue)
            {
                _failedSpend.OnNext(Unit.Default);
                return false;
            }

            _value.OnNext(_value.CurrentValue - amount);
            _spent.OnNext(Unit.Default);
            return true;
        }

        private void ValidateAmount(int amount)
        {
            if (amount > 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(amount));
        }
    }
}