using ArmorSystem.Down;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Down
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/FireDown")]
    public class DownFireArmorFactory : ArmorFactory<Legs>
    {
        public override Armor<Legs> Produce(Legs legs)
        {
            return new FireLegsArmor(legs, DamageReduceAmount);
        }
    }
}