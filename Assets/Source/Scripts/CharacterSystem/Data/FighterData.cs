using System.Collections.Generic;
using FightingSystem;
using FightingSystem.Attacks;
using HealthSystem;
using UnityEngine;

namespace CharacterSystem.Data
{
    public abstract class FighterData : MonoBehaviour
    {
        [SerializeField] private AttackData[] _attacks;
        
        [field: SerializeField] public Fighter Fighter { get; private set; }
        [field: SerializeField] public HealthView HealthView { get; private set; }
        [field: SerializeField] [field: Min(1)] public float StartHealthValue { get; private set; }
        [field: SerializeField] public Attacker Attacker { get; private set; }
        
        public IEnumerable<AttackData> Attacks => _attacks;
    }
}