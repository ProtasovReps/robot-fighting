using System;
using FiniteStateMachine.States;
using Interface;

namespace InputSystem.Bot
{
    public class BotSoloInput<TTargetState> : BotInput<TTargetState>
    where TTargetState : State

    {
        private readonly BotAction _botAction;
        
        public BotSoloInput(IStateMachine stateMachine, BotAction botAction)
            : base(stateMachine)
        {
            if (botAction == null)
                throw new ArgumentNullException(nameof(botAction));

            _botAction = botAction;
        }

        protected override BotAction GetAction()
        {
            return _botAction;
        }
    }
}