using System;
using FiniteStateMachine;
using FiniteStateMachine.States;
using HitSystem;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace Extensions
{
    public class BotColliderSwitcher : MonoBehaviour
    {
        [SerializeField] private HitColliderStash _hitColliderStash;
        
        [Inject]
        private void Inject(BotStateMachine botStateMachine)
        {
            Type specialAttackType = typeof(SpecialAttackState);
            
            botStateMachine.Value
                .Where(state => state.Type == specialAttackType)
                .Subscribe(_ => DisableColliders())
                .AddTo(this);

            botStateMachine.Value
                .Pairwise()
                .Where(pair => pair.Previous.Type == specialAttackType)
                .Subscribe(_ => EnableColliders())
                .AddTo(this);
        }
        
        private void EnableColliders()
        {
            _hitColliderStash.UpCollider.enabled = true;
            _hitColliderStash.DownCollider.enabled = true;
        }

        private void DisableColliders()
        {
            _hitColliderStash.UpCollider.enabled = false;
            _hitColliderStash.DownCollider.enabled = false;
        }
    }
}