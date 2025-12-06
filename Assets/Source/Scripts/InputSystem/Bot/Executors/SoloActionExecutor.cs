using System;
using FiniteStateMachine.States;
using Interface;

namespace InputSystem.Bot.Executor
{
    public class SoloActionExecutor<TTargetState> : ActionExecutor<TTargetState>
        where TTargetState : State
    {
        private readonly BotAction _botAction;

        public SoloActionExecutor(IStateMachine stateMachine, BotAction botAction)
            : base(stateMachine)
        {
            if (botAction == null)
            {
                throw new ArgumentNullException(nameof(botAction));
            }

            _botAction = botAction;
        }

        protected override BotAction GetAction()
        {
            return _botAction;
        }
    }
}