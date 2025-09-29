using System;
using CharacterSystem.Data;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace FightingSystem
{
    public class SpecialAttackColliderSwitcher : MonoBehaviour
    {
        [SerializeField] private BotData _botData;

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
            _botData.SkinData.ColliderStash.UpCollider.enabled = true;
            _botData.SkinData.ColliderStash.DownCollider.enabled = true;
        }

        private void DisableColliders()
        {
            _botData.SkinData.ColliderStash.UpCollider.enabled = false;
            _botData.SkinData.ColliderStash.DownCollider.enabled = false;
        }
    }
}