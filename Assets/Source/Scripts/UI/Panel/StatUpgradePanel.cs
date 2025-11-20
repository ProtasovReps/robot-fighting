using CharacterSystem;
using Extensions;
using UI.Buttons.StatUpgrade;
using UnityEngine;
using R3;
using Reflex.Attributes;

namespace UI.Panel
{
    public class StatUpgradePanel : MonoBehaviour
    {
        [SerializeField] private StatUpgradeButton[] _upgradeButtons;
        
        private DownCounter _downCounter;
        private CharacterStats _stats;

        [Inject]
        private void Inject(CharacterStats stats)
        {
            _stats = stats;
        }
        
        public void Initialize(DownCounter downCounter)
        {
            _downCounter = downCounter;
            
            foreach (StatUpgradeButton button in _upgradeButtons)
            {
                button.Pressed
                    .Subscribe(Upgrade)
                    .AddTo(this);
            }
        }

        private void Upgrade(StatUpgradeButton upgradeButton)
        {
            if (_downCounter.Value.CurrentValue == 0)
            {
                return;
            }

            _downCounter.Tick();
            upgradeButton.Upgrade(_stats);
        }
    }
}