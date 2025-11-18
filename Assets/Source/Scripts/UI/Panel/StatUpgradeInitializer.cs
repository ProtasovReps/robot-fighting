using System.Collections.Generic;
using CharacterSystem;
using Extensions;
using UI.Info;
using UnityEngine;
using YG;
using YG.Saver;

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
            Dictionary<StatType, float> startStats = new Dictionary<StatType, float>
            {
                { StatType.Health, YG2.saves.HealthStat },
                { StatType.Damage, YG2.saves.DamageStat },
                { StatType.Speed, YG2.saves.SpeedStat },
                { StatType.Block, YG2.saves.BlockStat }
            };

            CharacterStats stats = new(startStats);
            CharacterStatSaver saver = new(stats);
            
            _pointsView.Initialize(downCounter);
            _statUpgradePanel.Initialize(downCounter, stats);

            for (int i = 0; i < _statInfo.Length; i++)
            {
                _statInfo[i].Initialize(stats);
            }
        }
    }
}