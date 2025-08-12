using R3;

namespace InputSystem
{
    public class PlayerAttackInputReader
    {
        private readonly Subject<Unit> _punchPressed;
        private readonly Subject<Unit> _kickPressed;
        private readonly Subject<Unit> _blockPressed;
        
        public PlayerAttackInputReader(UserInput input)
        {
            _punchPressed = new Subject<Unit>();
            _kickPressed = new Subject<Unit>();
            _blockPressed = new Subject<Unit>();
            
            input.Player.Punch.performed += _ => _punchPressed.OnNext(Unit.Default);
            input.Player.Kick.performed += _ => _kickPressed.OnNext(Unit.Default);
            input.Player.Block.performed += _ => _blockPressed.OnNext(Unit.Default);
        }
        
        public Observable<Unit> PunchPressed => _punchPressed;
        public Observable<Unit> KickPressed => _kickPressed;
        public Observable<Unit> BlockPressed => _blockPressed;
    }
}