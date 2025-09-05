using Interface;

namespace CharacterSystem.FighterParts
{
    public class Legs : FighterPart
    {
        public Legs(IDamageable<float> damageable)
            : base(damageable)
        {
        }
    }
}