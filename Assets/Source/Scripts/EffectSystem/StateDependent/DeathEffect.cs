using FiniteStateMachine.States;
using UnityEngine;

namespace EffectSystem.StateDependent
{
    public class DeathEffect : StateDependentEffect<DeathState>
    {
        [SerializeField, Range(0.1f, 1f)] private float _targetTimeScale;
        [SerializeField] private Transform[] _objectsToDisable;
        
        protected override void Apply()
        {
            for (int i = 0; i < _objectsToDisable.Length; i++)
            {
                _objectsToDisable[i].gameObject.SetActive(false);
            }
            
            Time.timeScale = _targetTimeScale;
        }
    }
}