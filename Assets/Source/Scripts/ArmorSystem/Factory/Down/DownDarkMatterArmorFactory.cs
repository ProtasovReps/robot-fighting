using ArmorSystem.Down;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Down
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/DarkMatterDown")]
    public class DownDarkMatterArmorFactory : ArmorFactory<Legs>
    {
        public override Armor<Legs> Produce(Legs legs)
        {
            return new DarkMatterLegsArmor(legs, DamageReduceAmount);
        }
    }
}