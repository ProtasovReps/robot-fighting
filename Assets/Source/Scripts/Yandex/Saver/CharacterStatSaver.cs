using CharacterSystem;
using Extensions;
using Interface;

namespace YG.Saver
{
    public class CharacterStatSaver : ISaver
    {
        private readonly CharacterStats _stats;
        
        public CharacterStatSaver(CharacterStats stats)
        {
            _stats = stats;
        }
        
        public void Save()
        {
            YG2.saves.HealthStat = _stats.Get(StatType.Health);
            YG2.saves.DamageStat = _stats.Get(StatType.Damage);
            YG2.saves.SpeedStat = _stats.Get(StatType.Speed);
            YG2.saves.BlockStat = _stats.Get(StatType.Block);
        }
    }
}