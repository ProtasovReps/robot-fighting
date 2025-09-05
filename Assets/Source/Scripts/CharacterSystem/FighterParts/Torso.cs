using Interface;

namespace CharacterSystem.FighterParts
{
    public class Torso : FighterPart
    {
        public Torso(IDamageable<float> damageable)
            : base(damageable)
        {
        }
    }
}