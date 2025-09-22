using System;
using System.Collections.Generic;
using Extensions;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace InputSystem.Bot
{
    public class BotNothingNearbyActionExecutor : BotRandomActionExecutor<NothingNearbyState>
    {
        private readonly IDisposable _subscription;
        private readonly Dictionary<int, BotAction> _directions;
        private readonly BotMoveInput _botMoveInput;

        private Type _lastState;

        public BotNothingNearbyActionExecutor(
            IStateMachine stateMachine, BotMoveInput botMoveInput, BotAction left, BotAction right, BotAction inPlace)
            : base(stateMachine, left, right, inPlace)
        {
            _directions = new Dictionary<int, BotAction>(3)
            {
                { Directions.Left, left },
                { Directions.Right, right },
                { Directions.InPlace, inPlace }
            };

            _subscription = stateMachine.Value
                .Pairwise()
                .Where(pair => pair.Previous != null)
                .Subscribe(pair => _lastState = pair.Previous.Type);

            ResetLastState();
            _botMoveInput = botMoveInput;
        }

        public override void Dispose()
        {
            _subscription.Dispose();
            base.Dispose();
        }

        protected override BotAction GetAction()
        {
            if (_lastState == typeof(NothingNearbyState))
                return base.GetAction();

            int direction = 0;

            if (_lastState == typeof(OpponentNearbyState) && _botMoveInput.Value.CurrentValue == Directions.Right)
                direction = Directions.Right;

            if (_lastState == typeof(WallNearbyState) && _botMoveInput.Value.CurrentValue == Directions.Left)
                direction = Directions.Left;
                
            ResetLastState();
            return _directions[direction];
        }

        private void ResetLastState()
        {
            _lastState = typeof(NothingNearbyState);
        }
    }
}