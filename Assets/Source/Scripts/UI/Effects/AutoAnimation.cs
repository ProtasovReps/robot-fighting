using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Effect
{
    public class AutoAnimation : MonoBehaviour
    {
        [SerializeField] private Animatable _animatable;
        [SerializeField] private float _delay;
        
        private void Start()
        {
            AnimateDelayed().Forget();
        }

        private async UniTaskVoid AnimateDelayed()
        {
            await UniTask.WaitForSeconds(_delay);
            await _animatable.Play();
        }
    }
}