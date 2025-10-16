using System;
using Extensions;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class AttackImplant : MonoBehaviour
    {
        [SerializeField] private DamageFactory _damageFactory;
        [SerializeField] private AttackData _attackData;

        public abstract Type RequiredState { get; }
        public abstract AttackPart RequiredPart { get; }
        public AnimationClip AnimationClip => _attackData.Clip;
        
        public Attack GetAttack(LayerMask opponentLayerMask, Damage baseDamage)
        {
            Damage damage = _damageFactory.Produce(baseDamage);
            return ConstructAttack(damage, _attackData.Duration, _attackData.Delay, opponentLayerMask);
        }

        protected abstract Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask);
    }
}