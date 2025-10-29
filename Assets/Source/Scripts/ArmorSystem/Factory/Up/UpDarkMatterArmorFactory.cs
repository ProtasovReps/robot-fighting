using ArmorSystem.Up;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Up
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/DarkMatterUp")]
    public class UpDarkMatterArmorFactory : ArmorFactory<Torso>
    {
        public override Armor<Torso> Produce(Torso torso)
        {
            return new DarkMatterTorsoArmor(torso, DamageReduceAmount);
        }
    }
}