using Extensions;
using R3;
using UI.Info;
using UI.Panel;
using UnityEngine;
using UnityEngine.UI;
using YG.Awards;

namespace UI.LevelEnd
{
    public class StatUpgradeInitializer : MonoBehaviour
    {
        [SerializeField] private StatUpgradePanel _statUpgradePanel;
        [SerializeField] private IntegerView _pointsView;
        [SerializeField, Min(1)] private int _skillPoints;
        [SerializeField] private ExtraUpgradeAwardPanel _awardPanel;
        [SerializeField] private Button _continueButton;
        
        private void Awake()
        {
            DownCounter counter = new();
            
            counter.Reset();
            counter.AddPoints(_skillPoints);

            counter.Value
                .Where(value => value == 0)
                .Subscribe(_ => _continueButton.interactable = true)
                .AddTo(this);
            
            _awardPanel.Initialize(counter);
            _pointsView.Initialize(counter);
            _statUpgradePanel.Initialize(counter);
        }
    }
}