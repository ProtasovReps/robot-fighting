using System;
using System.Collections.Generic;
using Extensions;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using ImplantSystem;
using ImplantSystem.AttackImplants;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace FightingSystem.Factory
{
    public abstract class AttackFactory : MonoBehaviour
    {
        [SerializeField] private Attacker _attacker;
        [SerializeField] private LayerMask _opponentLayer;

        public void Produce(ImplantPlaceHolderStash implantPlaceHolderStash)
        {
            Dictionary<Type, Attack> attacks = new();
           
            Damage baseDamage = GetBaseDamage();

            foreach (ImplantPlaceHolder placeHolder in implantPlaceHolderStash.ActivePlaceHolders)
            {
                foreach (AttackImplant implant in placeHolder.Implants)
                {
                    Type attackState = AttackStateComparer.GetAttackState(implant.AttackParameters.RequiredState);

                    if (attacks.ContainsKey(attackState))
                        throw new ArgumentOutOfRangeException(nameof(attackState));

                    attacks.Add(attackState, implant.GetAttack(_opponentLayer, baseDamage));
                }
            }

            _attacker.SetAttacks(attacks);
        }

        protected abstract Damage GetBaseDamage();
    }
}