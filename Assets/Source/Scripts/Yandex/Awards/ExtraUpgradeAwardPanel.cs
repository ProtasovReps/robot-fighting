using Extensions;
using R3;
using UnityEngine;

namespace YG.Awards
{
    public class ExtraUpgradeAwardPanel : AwardPanel
    {
        [SerializeField, Min(1)] private int _addAmount;
        
        private DownCounter _downCounter;

        public void Initialize(DownCounter downCounter)
        {
            _downCounter = downCounter;
            
            _downCounter.Ended
                .Subscribe(_ => SetEnable(false))
                .AddTo(this);
        }
        
        protected override void AddAward()
        {
            _downCounter.AddPoints(_addAmount);
            SetEnable(false);
        }
    }
}