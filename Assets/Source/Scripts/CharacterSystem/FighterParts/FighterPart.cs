using Interface;
using R3;

namespace CharacterSystem.FighterParts
{
    public abstract class FighterPart : IDamageable
    {
        private readonly IDamageable _damageable;
        private readonly Subject<float> _hitted;

        protected FighterPart(IDamageable damageable)
        {
            _hitted = new Subject<float>();
            _damageable = damageable;
        }
        
        public Observable<float> Hitted => _hitted;
        
        public void AcceptDamage(float damage)
        {
            _hitted.OnNext(damage);
            _damageable.AcceptDamage(damage);
        }
    }
}