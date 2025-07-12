using CharacterSystem.Data;
using FiniteStateMachine.Factory;
using Interface;

namespace CharacterSystem.Factory
{
    public class BotFactory : FighterFactory
    {
        private readonly BotData _botData;
        
        public BotFactory(BotData fighterData) : base(fighterData)
        {
            _botData = fighterData;
        }

        protected override IStateMachine GetStateMachine()
        {
            return new BotStateMachineFactory(_botData).Produce();
        }
    }
}