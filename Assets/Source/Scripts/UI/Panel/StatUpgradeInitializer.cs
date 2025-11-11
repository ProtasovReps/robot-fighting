using CharacterSystem;
using Extensions;
using UI.Info;
using UnityEngine;

namespace UI.Panel
{
    public class StatUpgradeInitializer : MonoBehaviour
    {
        [SerializeField] private StatUpgradePanel _statUpgradePanel;
        [SerializeField] private IntegerView _pointsView;
        [SerializeField, Min(1)] private int _skillPoints;
        [SerializeField] private StatInfo[] _statInfo;
        
        private void Awake()
        {
            DownCounter downCounter = new(_skillPoints);
            CharacterStats stats = new();
            
            _pointsView.Initialize(downCounter);
            _statUpgradePanel.Initialize(downCounter, stats);

            for (int i = 0; i < _statInfo.Length; i++)
            {
                _statInfo[i].Initialize(stats);
            }
        }
    }
}