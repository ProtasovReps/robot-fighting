using FiniteStateMachine;
using FiniteStateMachine.States;
using Reflex.Attributes;
using UnityEngine;
using R3;

namespace CameraEffectSystem
{
    [RequireComponent(typeof(Camera))]
    public class HitEffect : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 0.1f)] private float _strength;
        
        private Transform _transform;
        private float _vectorLength;
        
        [Inject]
        private void Inject(BotStateMachine botStateMachine, PlayerStateMachine playerStateMachine)
        {
            Observable<State> botHitted = botStateMachine.Value
                .Where(value => value is HittedState); 
            
            Observable<State> playerHitted = playerStateMachine.Value
                .Where(value => value is HittedState);

            Observable.Merge(botHitted, playerHitted)
                .Subscribe(_ => Apply())
                .AddTo(this);
        }
        
        private void Awake()
        {
            _transform = transform;
            _vectorLength = 1 - _strength;
        }

        private void Apply()
        {
            _transform.position *= _vectorLength;
        }
    }
}