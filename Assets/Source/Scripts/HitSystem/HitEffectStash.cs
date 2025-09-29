using UnityEngine;

namespace HitSystem
{
    public class HitEffectStash : MonoBehaviour
    {
        [field: SerializeField] public ParticleSystem UpParticleEffect { get; private set; }
        [field: SerializeField] public ParticleSystem DownParticleEffect { get; private set; }
    }
}