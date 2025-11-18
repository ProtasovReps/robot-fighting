using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private SoundID _sound;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(Play);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Play);
        }

        private void Play()
        {
            BroAudio.Play(_sound);
        }        
    }
}