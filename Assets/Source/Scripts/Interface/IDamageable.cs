namespace Interface
{
    public interface IDamageable<in T>
    {
        void AcceptDamage(T damage);
    }
}
