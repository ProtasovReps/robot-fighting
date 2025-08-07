using System;
using FiniteStateMachine.States;
using Interface;
using Random = UnityEngine.Random;

namespace InputSystem.Bot
{
    public class BotRandomInput<TTargetState> : BotInput<TTargetState>
    where TTargetState : State
    {
        private readonly BotAction[] _botActions;
        
        public BotRandomInput(IStateMachine stateMachine, params BotAction[] botActions)
            : base(stateMachine)
        {
            if (botActions == null)
                throw new ArgumentNullException(nameof(botActions));

            _botActions = botActions;
        }

        protected override BotAction GetAction()
        {
            int randomIndex = Random.Range(0, _botActions.Length);
            return _botActions[randomIndex];
        }
    }
}