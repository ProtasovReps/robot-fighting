using ArmorSystem.Up;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Up
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/PlasmaUp")]
    public class UpPlasmaArmorFactory : ArmorFactory<Torso>
    {
        public override Armor<Torso> Produce(Torso torso)
        {
            return new PlasmaTorsoArmor(torso, DamageReduceAmount);
        }
    }
}