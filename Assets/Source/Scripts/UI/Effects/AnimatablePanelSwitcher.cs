using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace UI.Effect
{
    public class AnimatablePanelSwitcher : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _disappearAnimation;
        [SerializeField] private ScaleAnimation _appearAnimation;
        [SerializeField] private float _switchDelay;

        private IDisposable _subscription;

        public void Initialize(Observable<Unit> observable)
        {
            _subscription = observable
                .Delay(TimeSpan.FromSeconds(_switchDelay))
                .Subscribe(_ => Switch().Forget());
        }

        private async UniTaskVoid Switch()
        {
            _subscription.Dispose();
            await _disappearAnimation.Play();
            
            _disappearAnimation.gameObject.SetActive(false);
            await _appearAnimation.Play();
        }
    }
}