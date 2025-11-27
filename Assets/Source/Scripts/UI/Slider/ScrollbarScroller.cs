using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SliderView
{
    public class ScrollbarScroller : MonoBehaviour
    {
        private const int ScrollbarMaxValue = 1;
        
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private float _delay;
        [SerializeField] private float _scrollDuration;

        public async UniTaskVoid Scroll()
        {
            float elapsedTime = 0f;
            
            await UniTask.WaitForSeconds(_delay, true);

            while (elapsedTime < _scrollDuration)
            {
                _scrollbar.value = Mathf.Lerp(0, ScrollbarMaxValue, elapsedTime / _scrollDuration);
                
                elapsedTime += Time.unscaledDeltaTime;
                await UniTask.Yield();
            }
        }
    }
}