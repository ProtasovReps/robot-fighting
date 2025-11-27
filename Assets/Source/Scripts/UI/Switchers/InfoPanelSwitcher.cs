using Cysharp.Threading.Tasks;
using UI.SliderView;
using UnityEngine;

namespace UI.Effect
{
    public class InfoPanelSwitcher : AnimatablePanelSwitcher
    {
        [SerializeField] private ScrollbarScroller _scrollbarScroller;
        
        protected override UniTaskVoid Switch()
        {
            _scrollbarScroller.Scroll().Forget();
            return base.Switch();
        }
    }
}