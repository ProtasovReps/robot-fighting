using Extensions;
using R3;
using UI.Effect;
using UI.Info;
using UI.Panel;
using UnityEngine;

namespace UI.VictoryMenu
{
    public class StatUpgradeInitializer : MonoBehaviour
    {
        [SerializeField] private StatUpgradePanel _statUpgradePanel;
        [SerializeField] private IntegerView _pointsView;
        [SerializeField, Min(1)] private int _skillPoints;
        [SerializeField] private AnimatablePanelSwitcher _animatableSwitcher;

        private void Awake()
        {
            DownCounter downCounter = new(_skillPoints);
           
            Observable<Unit> switchMessage = downCounter.Value
                .Where(value => value == 0)
                .Select(_ => Unit.Default);
            
            _pointsView.Initialize(downCounter);
            _statUpgradePanel.Initialize(downCounter);
            _animatableSwitcher.Initialize(switchMessage);
        }
    }
}