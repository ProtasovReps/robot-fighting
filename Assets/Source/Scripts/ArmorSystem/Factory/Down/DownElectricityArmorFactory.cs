using ArmorSystem.Down;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Down
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/ElectricityDown")]
    public class DownElectricityArmorFactory : ArmorFactory<Legs>
    {
        public override Armor<Legs> Produce(Legs legs)
        {
            return new ElectricityLegsArmor(legs, DamageReduceAmount);
        }
    }
}