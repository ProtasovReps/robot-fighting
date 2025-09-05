using System;
using FightingSystem;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class AttackImplant : MonoBehaviour
    {
        [SerializeField] private DamageData _damageData;
        [SerializeField] private AttackData _attackData;
        
        public abstract Type RequiredState { get; }

        public Attack GetAttack(LayerMask opponentLayerMask)
        {
            Damage damage = new(_damageData.Damage, _damageData.ImpulseForce, _damageData.Type);
            return ConstructAttack(damage, _attackData, opponentLayerMask);
        }

        protected abstract Attack ConstructAttack(Damage damage, AttackData attackData, LayerMask layerMask);
    }
}