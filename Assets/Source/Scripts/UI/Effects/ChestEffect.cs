using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Effect
{
    public class ChestEffect : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _chest;
        [SerializeField] private ScaleAnimation _reward;
        [SerializeField] private SoundID _moneySound;
        
        public async UniTaskVoid StartEffect()
        {
            await _chest.Play();
            _chest.gameObject.SetActive(false);
            
            BroAudio.Play(_moneySound);
            await _reward.Play();
        }
    }
}