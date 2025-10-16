using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory
{
    public abstract class ArmorFactory<TFighterPart> : ScriptableObject
        where TFighterPart : DamageableFighterPart
    {
        [field: SerializeField, Min(0f)] public float DamageReduceAmount { get; private set; }
        
        public abstract Armor<TFighterPart> Produce(TFighterPart fighterPart);
    }
}