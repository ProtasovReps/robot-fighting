using System;
using FiniteStateMachine.States;
using HitSystem;
using Interface;
using R3;
using UnityEngine;

namespace Extensions
{
    public class ColliderSwitcher : MonoBehaviour
    {
        private HitColliderStash _colliderStash;
        
        public void Initialize(IStateMachine stateMachine, HitColliderStash colliderStash)
        {
            Type superState = typeof(SuperAttackState);
            Type specialState = typeof(SpecialAttackState);

            _colliderStash = colliderStash;
            Func<State, bool> stateMatched = state => state.Type == superState || state.Type == specialState;
            
            stateMachine.Value
                .Where(stateMatched)
                .Subscribe(_ => DisableColliders())
                .AddTo(this);
        
            stateMachine.Value
                .Pairwise()
                .Where(pair => stateMatched(pair.Previous))
                .Where(pair => stateMatched(pair.Current) == false)
                .Subscribe(_ => EnableColliders())
                .AddTo(this);
        }
        
        private void EnableColliders()
        {
            _colliderStash.UpCollider.gameObject.SetActive(true);
            _colliderStash.DownCollider.gameObject.SetActive(true);
        }
        
        private void DisableColliders()
        {
            _colliderStash.UpCollider.gameObject.SetActive(false);
            _colliderStash.DownCollider.gameObject.SetActive(false);
        }
    }
}