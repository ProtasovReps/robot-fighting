using System;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace FightingSystem
{
    public class SpecialAttackColliderSwitcher : MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;

        [Inject]
        private void Inject(BotStateMachine botStateMachine)
        {
            Type searchedType = typeof(SpecialAttackState);
            
            botStateMachine.Value
                .Where(state => state.Type == searchedType)
                .Subscribe(_ => DisableColliders())
                .AddTo(this);

            botStateMachine.Value
                .Pairwise()
                .Where(pair => pair.Previous.Type == searchedType)
                .Subscribe(_ => EnableColliders())
                .AddTo(this);
        }
        
        private void EnableColliders()
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = true;
            }
        }

        private void DisableColliders()
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = false;
            }
        }
    }
}