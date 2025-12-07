using System;
using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using UI.Buttons;
using UI.Effect;
using UnityEngine;
using R3;
using Reflex.Attributes;
using YG;
using YG.Saver;

namespace UI.LevelChange
{
    public abstract class SceneTransition : MonoBehaviour
    {
        private const float DefaultTimeScale = 1f;
        
        [SerializeField] private UnitButton _unitButton;
        [SerializeField] private ScaleAnimation _scaleAnimation;
        
        private ProgressSaver _progressSaver;
        private IDisposable _subscription;

        [Inject]
        private void Inject(ProgressSaver saver)
        {
            _progressSaver = saver;
        }
        
        private void Awake()
        {
            _subscription = _unitButton.Pressed
                .Subscribe(_ => ShowAd());
        }

        protected abstract void LoadScene();

        private void ShowAd()
        {
            BroAudio.Stop(BroAudioType.All);
            YG2.InterstitialAdvShow();
            Transit().Forget();
        }
        
        private async UniTaskVoid Transit()
        {
            _subscription.Dispose();
            _progressSaver.Save();
            _scaleAnimation.gameObject.SetActive(true);
            
            await _scaleAnimation.Play();
            
            Time.timeScale = DefaultTimeScale;
            LoadScene();
        }
    }
}