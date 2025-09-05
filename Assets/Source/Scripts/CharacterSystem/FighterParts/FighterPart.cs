using FightingSystem;
using Interface;
using R3;

namespace CharacterSystem.FighterParts
{
    public abstract class FighterPart : IDamageable<Damage>
    {
        private readonly IDamageable<float> _damageable;
        private readonly Subject<Damage> _hitted;
        
        protected FighterPart(IDamageable<float> damageable)
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