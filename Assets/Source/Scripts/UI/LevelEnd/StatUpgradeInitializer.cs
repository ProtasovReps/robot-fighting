using Extensions;
using UI.Effect;
using UI.Info;
using UI.Panel;
using UnityEngine;
using YG.Awards;

namespace UI.LevelEnd
{
    public class StatUpgradeInitializer : MonoBehaviour
    {
        [SerializeField] private StatUpgradePanel _statUpgradePanel;
        [SerializeField] private IntegerView _pointsView;
        [SerializeField, Min(1)] private int _skillPoints;
        [SerializeField] private AnimatablePanelSwitcher _animatableSwitcher;
        [SerializeField] private ExtraUpgradeAwardPanel _awardPanel;
        
        private void Awake()
        {
            DownCounter counter = new();
            
            counter.Reset();
            counter.AddPoints(_skillPoints);
           
            _awardPanel.Initialize(counter);
            _pointsView.Initialize(counter);
            _statUpgradePanel.Initialize(counter);
            _animatableSwitcher.Initialize(counter.Ended);
        }
    }
}