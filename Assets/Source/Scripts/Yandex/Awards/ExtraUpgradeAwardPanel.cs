using Extensions;
using UnityEngine;

namespace YG.Awards
{
    public class ExtraUpgradeAwardPanel : AwardPanel
    {
        private const string RewardPointsWatched = nameof(RewardPointsWatched);
        
        [SerializeField] [Min(1)] private int _addAmount;
        
        private DownCounter _downCounter;

        public void Initialize(DownCounter downCounter)
        {
            _downCounter = downCounter;
        }
        
        protected override void AddAward()
        {
            YG2.MetricaSend(RewardPointsWatched);
            
            _downCounter.AddPoints(_addAmount);
            SetEnable(false);
        }
    }
}