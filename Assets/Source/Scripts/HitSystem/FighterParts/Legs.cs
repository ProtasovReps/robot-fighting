using Interface;

namespace HitSystem.FighterParts
{
    public class Legs : DamageableFighterPart
    {
        public Legs(IDamageable<float> damageable)
            : base(damageable)
        {
        }
    }
}