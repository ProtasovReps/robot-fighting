using ArmorSystem.Down;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Down
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/RustyDown")]
    public class DownRustyArmorFactory : ArmorFactory<Legs>
    {
        public override Armor<Legs> Produce(Legs legs)
        {
            return new RustyLegsArmor(legs, DamageReduceAmount);
        }
    }
}