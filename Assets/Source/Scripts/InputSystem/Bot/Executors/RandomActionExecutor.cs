using System;
using FiniteStateMachine.States;
using Interface;
using Random = UnityEngine.Random;

namespace InputSystem.Bot.Executor
{
    public class RandomActionExecutor<TTargetState> : ActionExecutor<TTargetState>
        where TTargetState : State
    {
        private readonly BotAction[] _botActions;

        public RandomActionExecutor(IStateMachine stateMachine, params BotAction[] botActions)
            : base(stateMachine)
        {
            if (botActions.Length == 0)
            {
                throw new ArgumentException(nameof(botActions));
            }

            _botActions = botActions;
        }

        protected override BotAction GetAction()
        {
            int randomIndex = Random.Range(0, _botActions.Length);
            return _botActions[randomIndex];
        }
    }
}