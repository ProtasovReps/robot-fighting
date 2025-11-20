using System;
using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using R3;
using UI.Buttons;
using UI.Effect;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.LevelChange
{
    public class CustomizationSceneTransition : MonoBehaviour
    {
        private const float DefaultTimeScale = 1f;
        
        [SerializeField] private CustomizationSceneName _sceneName;
        [SerializeField] private UnitButton _unitButton;
        [SerializeField] private ScaleAnimation _scaleAnimation;
        
        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = _unitButton.Pressed
                .Subscribe(_ => Transit().Forget());
        }

        private async UniTaskVoid Transit()
        {
            _subscription.Dispose();

            string nextSceneName = _sceneName.ToString();

            await _scaleAnimation.Play();
            Time.timeScale = DefaultTimeScale;
            BroAudio.Stop(BroAudioType.Music);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}