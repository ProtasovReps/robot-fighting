using CharacterSystem.Data;
using FiniteStateMachine.Factory;
using Interface;

namespace CharacterSystem.Factory
{
    public class PlayerFactory : FighterFactory
    {
        private readonly PlayerData _playerData;
        
        public PlayerFactory(PlayerData fighterData) : base(fighterData)
        {
            _playerData = fighterData;
        }

        protected override IStateMachine GetStateMachine()
        {
            return new PlayerStateMachineFactory(_playerData).Produce();
        }
    }
}