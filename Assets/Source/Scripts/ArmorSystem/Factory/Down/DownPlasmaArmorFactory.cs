using ArmorSystem.Down;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Down
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/PlasmaDown")]
    public class DownPlasmaArmorFactory : ArmorFactory<Legs>
    {
        public override Armor<Legs> Produce(Legs legs)
        {
            return new PlasmaLegsArmor(legs, DamageReduceAmount);
        }
    }
}