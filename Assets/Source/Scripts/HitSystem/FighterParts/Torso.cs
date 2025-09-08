using Interface;

namespace HitSystem.FighterParts
{
    public class Torso : DamageableFighterPart
    {
        public Torso(IDamageable<float> damageable)
            : base(damageable)
        {
        }
    }
}