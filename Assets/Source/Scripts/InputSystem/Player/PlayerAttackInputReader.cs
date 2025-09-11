using R3;

namespace InputSystem
{
    public class PlayerAttackInputReader
    {
        public PlayerAttackInputReader(UserInput input)
        {
            var punchPressed = new Subject<Unit>();
            var kickPressed = new Subject<Unit>();
            var blockPressed = new Subject<Unit>();
            var superPressed = new Subject<Unit>();

            PunchPressed = punchPressed;
            KickPressed = kickPressed;
            BlockPressed = blockPressed;
            SuperPressed = superPressed;
            
            input.Player.Punch.performed += _ => punchPressed.OnNext(Unit.Default);
            input.Player.Kick.performed += _ => kickPressed.OnNext(Unit.Default);
            input.Player.Block.performed += _ => blockPressed.OnNext(Unit.Default);
            input.Player.Super.performed += _ => superPressed.OnNext(Unit.Default);
        }

        public Observable<Unit> PunchPressed { get; }
        public Observable<Unit> KickPressed { get; }
        public Observable<Unit> BlockPressed { get; }
        public Observable<Unit> SuperPressed { get; }
    }
}