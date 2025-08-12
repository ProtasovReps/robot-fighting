using System;
using FiniteStateMachine.States;
using Interface;

namespace InputSystem.Bot
{
    public class BotSoloActionExecutor<TTargetState> : BotActionExecutor<TTargetState>
        where TTargetState : State
    {
        private readonly BotAction _botAction;

        public BotSoloActionExecutor(IStateMachine stateMachine, BotAction botAction)
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