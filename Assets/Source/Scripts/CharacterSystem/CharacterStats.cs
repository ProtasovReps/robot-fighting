using System;
using System.Collections.Generic;
using Extensions;
using R3;

namespace CharacterSystem
{
    public class CharacterStats
    {
        private readonly Dictionary<StatType, float> _stats;
        private readonly Subject<Unit> _upgraded;
        
        public CharacterStats(Dictionary<StatType, float> startStats)
        {
            _stats = startStats;

            _upgraded = new Subject<Unit>();
        }

        public Observable<Unit> Upgraded => _upgraded;
        
        public float Get(StatType targetStat)
        {
            ValidateDictionary(targetStat);
            return _stats[targetStat];
        }

        public void Increase(StatType targetStat, float addAmount)
        {
            if (addAmount <= 0f)
                throw new ArgumentOutOfRangeException(nameof(addAmount));
            
            ValidateDictionary(targetStat);
            
            _stats[targetStat] += addAmount;
            _upgraded.OnNext(Unit.Default);
        }

        private void ValidateDictionary(StatType targetStat)
        {
            if (_stats.ContainsKey(targetStat) == false)
                throw new KeyNotFoundException(nameof(targetStat));
        }
    }
}