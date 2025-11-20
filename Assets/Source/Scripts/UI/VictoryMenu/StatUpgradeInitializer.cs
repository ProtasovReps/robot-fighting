using System.Collections.Generic;
using CharacterSystem;
using Extensions;
using R3;
using Reflex.Attributes;
using UI.Effect;
using UI.Info;
using UI.Panel;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.VictoryMenu
{
    public class StatUpgradeInitializer : MonoBehaviour
    {
        [SerializeField] private StatUpgradePanel _statUpgradePanel;
        [SerializeField] private IntegerView _pointsView;
        [SerializeField, Min(1)] private int _skillPoints;
        [SerializeField] private StatInfo[] _statInfo;
        [SerializeField] private AnimatablePanelSwitcher _animatableSwitcher;

        private ProgressSaver _progressSaver;
        
        [Inject]
        private void Inject(ProgressSaver saver)
        {
            _progressSaver = saver;
        }
        
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
            CharacterStatSaver statSaver = new(stats);
            Observable<Unit> switchMessage = downCounter.Value
                .Where(value => value == 0)
                .Select(_ => Unit.Default);
            
            _progressSaver.Add(statSaver);
            _pointsView.Initialize(downCounter);
            _statUpgradePanel.Initialize(downCounter, stats);
            _animatableSwitcher.Initialize(switchMessage);
            
            for (int i = 0; i < _statInfo.Length; i++)
            {
                _statInfo[i].Initialize(stats);
            }
        }
    }
}