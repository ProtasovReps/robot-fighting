using FiniteStateMachine.States;
using UnityEngine;

namespace EffectSystem.StateDependent
{
    public class HitEffect : StateDependentEffect<HittedState>
    {
        private const int OneMinus = 1;
        
        [SerializeField] [Range(0.01f, 0.1f)] private float _strength;
        [SerializeField] private Camera _camera;
        
        private Transform _transform;
        private float _vectorLength;
        
        private void Awake()
        {
            _transform = _camera.transform;
            _vectorLength = OneMinus - _strength;
        }

        protected override void Apply()
        {
            _transform.position *= _vectorLength;
        }
    }
}