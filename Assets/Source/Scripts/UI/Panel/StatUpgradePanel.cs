using CharacterSystem;
using Extensions;
using UI.Buttons.StatUpgrade;
using UnityEngine;
using R3;
using Reflex.Attributes;
using UI.Info;

namespace UI.Panel
{
    public class StatUpgradePanel : MonoBehaviour
    {
        [SerializeField] private StatUpgradeButton[] _upgradeButtons;
        [SerializeField] private IntegerView _avaiblePoints;
        [SerializeField, Min(1)] private int _skillPoints;

        private CharacterStats _stats;
        private DownCounter _downCounter;

        [Inject]
        private void Inject(CharacterStats stats)
        {
            _stats = stats;
        }

        private void Awake()
        {
            _downCounter = new DownCounter(_skillPoints);

            _avaiblePoints.Initialize(_downCounter);
            
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