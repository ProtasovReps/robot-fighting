using Ami.BroAudio;
using R3;
using UnityEngine;

namespace AudioSystem.EventDependent
{
    public abstract class EventSound : MonoBehaviour
    {
        [SerializeField] private SoundID _soundID;

        private void Start()
        {
            Observable<Unit> observable = GetObservable();

            observable
                .Subscribe(_ => BroAudio.Play(_soundID))
                .AddTo(this);
        }

        protected abstract Observable<Unit> GetObservable();
    }
}
