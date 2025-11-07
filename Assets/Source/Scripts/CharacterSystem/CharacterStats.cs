using System;
using System.Collections.Generic;
using Extensions;
using R3;
using YG;

namespace CharacterSystem
{
    public class CharacterStats
    {
        private readonly Dictionary<StatType, float> _stats;
        private readonly Subject<Unit> _upgraded;
        
        public CharacterStats()
        {
            _stats = new Dictionary<StatType, float>
            {
                { StatType.Health, YG2.saves.HealthStat },
                { StatType.Damage, YG2.saves.DamageStat },
                { StatType.Speed, YG2.saves.SpeedStat },
                { StatType.Block, YG2.saves.BlockStat }
            };

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