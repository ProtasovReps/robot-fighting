using System.Collections.Generic;
using FightingSystem.Attacks;
using UnityEngine;

namespace CharacterSystem.Data
{
    public abstract class FighterData : MonoBehaviour
    {
        [SerializeField] private AttackData[] _attacks;
        
        [field: SerializeField] [field: Min(1f)] public float StartHealthValue { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float StunDuration { get; private set; }
        [field: SerializeField] [field: Min(1f)] public float BlockDuration { get; private set; }
        
        public IEnumerable<AttackData> Attacks => _attacks;
    }
}