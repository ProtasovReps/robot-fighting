namespace Interface
{
    public interface IAttack
    {
        float Delay { get; }  // каждый тип аттаки лучше всего вхренячить в scriptable object, там хранить задержку перед нанесением урона
        
        void ApplyDamage(IDamageable damageable);
    }
}