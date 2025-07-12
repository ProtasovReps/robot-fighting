using Interface;
using Reflex.Attributes;

namespace MovementSystem
{
    public class BotPositionTranslation : PositionTranslation
    {
        private IBotStateMachine _botStateMachine;
        private IDirectionChangeable _directionChangeable;
        
        [Inject]
        private void Inject(IBotStateMachine stateMachine)
        {
            _botStateMachine = stateMachine;
        }

        private void Awake()
        {
            Initialize(_botStateMachine, _directionChangeable);
        }
        
        public void Initialize(IDirectionChangeable directionChangeable)
        {
            _directionChangeable = directionChangeable;
        }
    }
}