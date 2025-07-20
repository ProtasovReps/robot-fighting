using System;

namespace Interface
{
    public interface IAttack
    {
        float Delay { get; }
        float Duration { get; }
        Type RequiredState { get; }

        void ApplyDamage(IDamageable damageable);
    }
}