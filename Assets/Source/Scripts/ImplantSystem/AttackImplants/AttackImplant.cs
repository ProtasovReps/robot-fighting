using System;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class AttackImplant : MonoBehaviour
    {
        [SerializeField] private DamageData _damageData; // использовать по другому
        [SerializeField] private AttackData _attackData;

        public abstract Type RequiredState { get; }
        public abstract AttackPart RequiredPart { get; }
        public AnimationClip AnimationClip => _attackData.Clip;
        
        public Attack GetAttack(LayerMask opponentLayerMask) // вот сюда прокидывать данные урона
        {
            Damage damage = new(_damageData.Damage, _damageData.ImpulseForce, _damageData.Type);
            return ConstructAttack(damage, _attackData.Duration, _attackData.Delay, opponentLayerMask);
        }

        protected abstract Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask);
    }
}