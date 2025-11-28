using Cysharp.Threading.Tasks;
using UI.SliderView;
using UnityEngine;
using YG;

namespace UI.Effect
{
    public class InfoPanelSwitcher : AnimatablePanelSwitcher
    {
        [SerializeField] private LeaderboardYG _leaderboard;
        [SerializeField] private ScrollbarScroller _scrollbarScroller;
        [SerializeField] private PositionAnimation _levelAnimation;
        
        protected override UniTaskVoid Switch()
        {
            _leaderboard.gameObject.SetActive(true);
            _scrollbarScroller.Scroll().Forget();
            _levelAnimation.Play().Forget();
            return base.Switch();
        }
    }
}