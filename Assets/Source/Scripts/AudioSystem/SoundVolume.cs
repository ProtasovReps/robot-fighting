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
            
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            BroAudio.SetVolume(_slider.value);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            YG2.SaveProgress();
        }

        private void OnSliderValueChanged(float newValue)
        {
            _slider.value = newValue;
            
            YG2.saves.SoundVolume = _slider.value;
            BroAudio.SetVolume(newValue);
        }
    }
}