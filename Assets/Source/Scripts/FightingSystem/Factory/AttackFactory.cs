using System;
using System.Collections.Generic;
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

            foreach (ImplantPlaceHolder placeHolder in implantPlaceHolderStash.PlaceHolders)
            {
                foreach (AttackImplant attackImplant in placeHolder.Implants)
                {
                    if (attacks.ContainsKey(attackImplant.RequiredState))
                        throw new ArgumentOutOfRangeException(nameof(attackImplant.RequiredState));
                    
                    attacks.Add(attackImplant.RequiredState, attackImplant.GetAttack(_opponentLayer));
                }
            }
            
            _attacker.SetAttacks(attacks);
        }
    }
}