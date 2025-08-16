using System.Collections.Generic;
using CharacterSystem.Data;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using Interface;
using UnityEngine;

namespace CharacterSystem.Factory
{
    public abstract class AttackFactory<TData> : MonoBehaviour
        where TData : FighterData
    {
        [SerializeField] private Attacker _attacker;

        public void Produce(TData fighterData)
        {
            Dictionary<IAttack, Spherecaster> attacks = new();

            foreach (AttackData data in fighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Duration, data.Delay, 
                    AttackStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }

            _attacker.SetAttacks(attacks);
        }
    }
}