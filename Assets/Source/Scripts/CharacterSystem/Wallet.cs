using System;
using Interface;
using R3;
using YG;

namespace CharacterSystem
{
    public class Wallet : IValueChangeable<int>, IMoneySpendable
    {
        private readonly ReactiveProperty<int> _value;
        private readonly Subject<Unit> _failedSpend;
        
        public Wallet()
        {
            _value = new ReactiveProperty<int>(YG2.saves.Money);
            _failedSpend = new Subject<Unit>();
        }

        public ReadOnlyReactiveProperty<int> Value => _value;
        public Observable<Unit> FailedSpend => _failedSpend;

        public void Add(int amount)
        {
            ValidateAmount(amount);

            _value.OnNext(_value.CurrentValue + amount);
            YG2.saves.Money += amount; // мб отдельно в сейвер
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
            YG2.saves.Money -= amount; // мб отдельно в сейвер
            return true;
        }

        private void ValidateAmount(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
        }
    }
}