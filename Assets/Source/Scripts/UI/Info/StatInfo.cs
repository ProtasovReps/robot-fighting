using CharacterSystem;
using Extensions;
using UI.Buttons.StatUpgrade;
using UnityEngine;

namespace UI.Info
{
    public class StatInfo : ObservableInfo
    {
        [SerializeField] private StatUpgradeButton _upgradeButton;
        [SerializeField] private StatType _statType;

        private CharacterStats _stats;
        
        public void Initialize(CharacterStats characterStats)
        {
            _stats = characterStats;
            Initialize(_stats.Upgraded, () => _stats.Get(_statType));
        }
    }
}