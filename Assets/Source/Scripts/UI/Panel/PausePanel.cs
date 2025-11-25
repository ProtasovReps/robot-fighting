using Ami.BroAudio;
using R3;
using UI.Buttons;
using UnityEngine;
using YG;

namespace UI.Panel
{
    public class PausePanel : MonoBehaviour
    {
        private const float DefaultPitch = 1f;
        
        [SerializeField] private UnitButton _enableButton;
        [SerializeField] private UnitButton _disableButton;
        [SerializeField] private float _musicPitch;

        private void Awake()
        {
            _enableButton.Pressed
                .Subscribe(_ => Enable())
                .AddTo(this);

            _disableButton.Pressed
                .Subscribe(_ => Disable())
                .AddTo(this);

            YG2.onPauseGame += _ => Enable();
            
            Disable();
        }

        private void OnDestroy()
        {
            SetPitch(DefaultPitch);
        }

        private void Enable()
        {
            transform.gameObject.SetActive(true);
            SetPitch(_musicPitch);
            
            Time.timeScale = 0f;
        }

        private void Disable()
        {
            transform.gameObject.SetActive(false);
            SetPitch(DefaultPitch);
            
            Time.timeScale = 1f;
        }
        
        private void SetPitch(float pitch)
        {
            BroAudio.SetPitch(BroAudioType.Music, pitch);
        }
    }
}