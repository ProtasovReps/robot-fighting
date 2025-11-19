using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Effect
{
    public abstract class Animatable : MonoBehaviour
    {
        public abstract bool IsPlaying { get; }
        
        private void OnDestroy()
        {
            Cancel();
        }

        public abstract UniTask Play();
        
        protected abstract void Cancel();
    }
}