using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace AudioSystem
{
    public class SoundVolume : MonoBehaviour
    {
        private const float MaxSound = 0.05f;
        
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            _slider.maxValue = MaxSound;
            _slider.value = YG2.saves.SoundVolume;
            
            BroAudio.SetVolume(_slider.value);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnDestroy()
        {
            YG2.saves.SoundVolume = _slider.value;
            YG2.SaveProgress();
        }

        private void OnSliderValueChanged(float newValue)
        {
            _slider.value = newValue;

            BroAudio.SetVolume(newValue);
        }
    }
}