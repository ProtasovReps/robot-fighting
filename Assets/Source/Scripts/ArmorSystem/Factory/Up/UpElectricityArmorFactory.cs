using ArmorSystem.Up;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Up
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/ElectricityUp")]
    public class UpElectricityArmorFactory : ArmorFactory<Torso>
    {
        public override Armor<Torso> Produce(Torso torso)
        {
            return new ElectricityTorsoArmor(torso, DamageReduceAmount);
        }
    }
}