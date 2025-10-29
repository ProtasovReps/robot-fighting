using ArmorSystem.Up;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem.Factory.Up
{
    [CreateAssetMenu(fileName = "ArmorParameters", menuName = "Armor/Factory/FireUp")]
    public class UpFireArmorFactory : ArmorFactory<Torso>
    {
        public override Armor<Torso> Produce(Torso torso)
        {
            return new FireTorsoArmor(torso, DamageReduceAmount);
        }
    }
}