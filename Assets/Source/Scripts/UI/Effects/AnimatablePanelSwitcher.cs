using System;
using Cysharp.Threading.Tasks;
using R3;
using UI.Buttons;
using UnityEngine;

namespace UI.Effect
{
    public class AnimatablePanelSwitcher : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _disappearAnimation;
        [SerializeField] private ScaleAnimation _appearAnimation;
        [SerializeField] private UnitButton _unitButton;
        
        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = _unitButton.Pressed
                .Subscribe(_ => Switch().Forget());
        }

        protected virtual async UniTaskVoid Switch()
        {
            _subscription.Dispose();
            await _disappearAnimation.Play();
            
            _disappearAnimation.gameObject.SetActive(false);
            await _appearAnimation.Play();
        }
    }
}