using Interface;
using Reflex.Attributes;

namespace MovementSystem
{
    public class PlayerPositionTranslation : PositionTranslation
    {
        private IPlayerStateMachine _playerStateMachine;
        private IDirectionChangeable _directionChangeable;
        
        [Inject]
        private void Inject(IPlayerStateMachine stateMachine)
        {
            _playerStateMachine = stateMachine;
        }

        private void Awake()
        {
            Initialize(_playerStateMachine, _directionChangeable);
        }
        
        public void Initialize(IDirectionChangeable directionChangeable)
        {
            _directionChangeable = directionChangeable;
        }
    }
}