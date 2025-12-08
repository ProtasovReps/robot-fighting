using UnityEngine;
using YG;

namespace UI.LevelChange
{
    public class RestartGameTransition : SceneTransition
    {
        private const int FirstSceneIndex = 3;

        [SerializeField] private float _healthStat = 80f;
        [SerializeField] private float _speedStat = 7f;
        [SerializeField] private float _damageStat = 15f;
        [SerializeField] private float _blockStat = 20;
        
        protected override void LoadScene()
        {
            YG2.saves.HealthStat = _healthStat;
            YG2.saves.SpeedStat = _speedStat;
            YG2.saves.DamageStat = _damageStat;
            YG2.saves.BlockStat = _blockStat;
            YG2.saves.SceneIndex = FirstSceneIndex;
            YG2.SaveProgress();
        }
    }
}