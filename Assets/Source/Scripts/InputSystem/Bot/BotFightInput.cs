using R3;

namespace InputSystem.Bot
{
    public class BotFightInput
    {
        private readonly Subject<Unit> _upAttack;
        private readonly Subject<Unit> _downAttack;
        private readonly Subject<Unit> _block;
        private readonly Subject<Unit> _specialAttack;
        
        public BotFightInput()
        {
            _upAttack = new Subject<Unit>();
            _downAttack = new Subject<Unit>();
            _block = new Subject<Unit>();
            _specialAttack = new Subject<Unit>();
        }

        public Observable<Unit> UpAttack => _upAttack;
        public Observable<Unit> DownAttack => _downAttack;
        public Observable<Unit> SpecialAttack => _specialAttack;
        public Observable<Unit> Block => _block;

        public void AttackUp()
        {
            _upAttack.OnNext(Unit.Default);
        }

        public void AttackDown()
        {
            _downAttack.OnNext(Unit.Default);
        }
        
        public void AttackSpecial()
        {
            _specialAttack.OnNext(Unit.Default);
        }
        
        public void BlockAttack()
        {
            _block.OnNext(Unit.Default);
        }
    }
}