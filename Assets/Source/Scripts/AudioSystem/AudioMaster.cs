using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioMaster : MonoBehaviour
    {
        private readonly float _minVolume = -80;
        private readonly float _maxVolume = 0;

        [SerializeField] private AudioSource _buttonSound;
        [SerializeField] private AudioMixer _audioMixer;

        private float _nowVolume;
        private string _soundType;

        public void SetSoundType(string soundType)
        {
            _soundType = soundType;
        }

        public void OnVolumeChanges(float volume)
        {
            float nowVolume = _nowVolume;

            _audioMixer.SetFloat(_soundType, nowVolume = Mathf.Lerp(_minVolume, _maxVolume, volume));

            _nowVolume = nowVolume;
        }

        public void OnClick()
        {
            _buttonSound.Play();
        }
    }
}