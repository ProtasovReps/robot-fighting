using Interface;

namespace ArmorSystem
{
    public abstract class NoArmor : IArmor
    {
        public float ValidateDamage(float damage)
        {
            return damage;
        }
    }
}