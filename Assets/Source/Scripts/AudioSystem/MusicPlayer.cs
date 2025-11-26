using Ami.BroAudio;
using UnityEngine;
using YG;

namespace AudioSystem
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private SoundID _music;
        
        private void Awake()
        {
            YG2.onPauseGame += isPaused => SetPause(isPaused);
            YG2.onFocusWindowGame += isFocused => SetPause(!isFocused);
            
            Play();
        }

        private void OnDestroy()
        {
            YG2.onPauseGame -= isPaused => SetPause(isPaused);
            YG2.onFocusWindowGame -= isFocused => SetPause(!isFocused);
            
            Stop();
        }

        private void SetPause(bool isPaused)
        {
            if(isPaused)
                Stop();
            else
                Play();
        }
        
        private void Play()
        {
            BroAudio.SetVolume(YG2.saves.SoundVolume);
            BroAudio.Play(_music);
        }

        private void Stop()
        {
            BroAudio.Stop(_music);
        }
    }
}