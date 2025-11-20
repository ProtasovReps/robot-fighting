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

        [Inject]
        private void Inject(CharacterStats stats)
        {
            Initialize(stats.Upgraded, () => stats.Get(_statType));
        }
    }
}