using CharacterSystem;
using Extensions;
using Reflex.Attributes;
using UI.Buttons.StatUpgrade;
using UnityEngine;

namespace UI.Info
{
    public class StatInfo : ObservableInfo
    {
        [SerializeField] private StatUpgradeButton _upgradeButton;
        [SerializeField] private StatType _statType;

        private CharacterStats _stats;
        
        [Inject]
        private void Inject(CharacterStats stats)
        {
            _stats = stats;
            Subscribe(_stats.Upgraded);
        }
        
        protected override float GetInfo()
        {
            return _stats.Get(_statType);
        }
    }
}