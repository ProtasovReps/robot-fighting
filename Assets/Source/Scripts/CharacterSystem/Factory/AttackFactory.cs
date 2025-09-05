using System;
using System.Collections.Generic;
using FightingSystem;
using FightingSystem.Attacks;
using ImplantSystem;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace CharacterSystem.Factory
{
    public abstract class AttackFactory : MonoBehaviour
    {
        [SerializeField] private ImplantPlaceHolder[] _implantPlaceHolders;
        [SerializeField] private AttackImplant[] _attackImplants; // берется из инвентаря
        [SerializeField] private Attacker _attacker;
        [SerializeField] private LayerMask _opponentLayer;
        
        // вообще, для каждой части тела должен быть один имплант, поэтому потом добавить и для специальной атаки
        public void Produce() 
        {
            Dictionary<Type, Attack> attacks = new();

            for (int i = 0; i < _implantPlaceHolders.Length; i++)
            {
                ImplantPlaceHolder placeHolder = _implantPlaceHolders[i];

                for (int j = 0; j < _attackImplants.Length; j++) // будет выглядеть иначе, если 1 плейсхолдер 1 имплант
                {
                    if(placeHolder.IsValidImplant(_attackImplants[j]) == false)
                        continue;

                    AttackImplant attackImplant = Instantiate(_attackImplants[j]);
                    
                    placeHolder.SetImplant(attackImplant);
                    attacks.Add(attackImplant.RequiredState, attackImplant.GetAttack(_opponentLayer));
                }
            }
            
            _attacker.SetAttacks(attacks);
        }
    }
}