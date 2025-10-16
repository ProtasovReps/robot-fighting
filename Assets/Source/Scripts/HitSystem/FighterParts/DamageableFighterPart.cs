using FightingSystem.AttackDamage;
using Interface;
using R3;

namespace HitSystem.FighterParts
{
    public abstract class DamageableFighterPart : FighterPart, IDamageable<Damage>
    {
        private readonly IDamageable<float> _damageable;
        private readonly Subject<Damage> _hitted;
        
        protected DamageableFighterPart(IDamageable<float> damageable)
        {
            _hitted = new Subject<Damage>();
            _damageable = damageable;
        }
        
        public Observable<Damage> Hitted => _hitted;
        
        public void AcceptDamage(Damage damage)
        {
            _hitted.OnNext(damage);
            _damageable.AcceptDamage(damage.Value);
        }
    }
}