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
            YG2.onPauseGame += isPaused => OnPauseGame(isPaused);

            Play();
        }

        private void OnDestroy()
        {
            YG2.onPauseGame -= isPaused => OnPauseGame(isPaused);

            Stop();
        }

        private void OnPauseGame(bool isPaused)
        {
            if(isPaused)
                Stop();
            else
                Play();
        }
        
        private void Play()
        {
            BroAudio.Play(_music);
        }

        private void Stop()
        {
            BroAudio.Stop(_music);
        }
    }
}