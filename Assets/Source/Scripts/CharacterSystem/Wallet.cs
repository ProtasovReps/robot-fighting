using System;
using Interface;
using R3;
using YG;

namespace CharacterSystem
{
    public class Wallet : IValueChangeable<int>
    {
        private readonly ReactiveProperty<int> _value;
        
        public Wallet()
        {
            _value = new ReactiveProperty<int>(YG2.saves.Money);
        }

        public ReadOnlyReactiveProperty<int> Value => _value;

        public void Add(int amount)
        {
            ValidateAmount(amount);

            _value.OnNext(_value.CurrentValue + amount);
            YG2.saves.Money += amount;
        }
        
        public void Spend(int amount)
        {
            ValidateAmount(amount);

            if (amount > _value.CurrentValue)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            _value.OnNext(_value.CurrentValue - amount);
            YG2.saves.Money -= amount;
        }

        private void ValidateAmount(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
        }
    }
}