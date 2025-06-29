using System;

namespace Interface
{
    public interface IAttack
    {
        float Delay { get; }
        Type RequiredState { get; }

        void ApplyDamage(IDamageable damageable);
    }
}