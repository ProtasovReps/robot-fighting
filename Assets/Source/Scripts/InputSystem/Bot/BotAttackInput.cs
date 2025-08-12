using R3;

namespace InputSystem.Bot
{
    public class BotAttackInput
    {
        private readonly Subject<Unit> _upAttack;
        private readonly Subject<Unit> _downAttack;
        
        public BotAttackInput()
        {
            _upAttack = new Subject<Unit>();
            _downAttack = new Subject<Unit>();
        }

        public Observable<Unit> UpAttack => _upAttack;
        public Observable<Unit> DownAttack => _downAttack;

        public void AttackUp()
        {
            _upAttack.OnNext(Unit.Default);
        }

        public void AttackDown()
        {
            _downAttack.OnNext(Unit.Default);
        }
    }
}