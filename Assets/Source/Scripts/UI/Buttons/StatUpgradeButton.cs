using CharacterSystem;
using Extensions;
using UnityEngine;
using YG;

namespace UI.Buttons.StatUpgrade
{
    public class StatUpgradeButton : ObservableButton<StatUpgradeButton>
    {
        [SerializeField] private float _upgradeValue;
        [SerializeField] private StatType _statType;

        public void Upgrade(CharacterStats stats)
        {
            stats.Increase(_statType, _upgradeValue);
            YG2.MetricaSend(_statType.ToString());
        }

        protected override StatUpgradeButton Get()
        {
            return this;
        }
    }
}