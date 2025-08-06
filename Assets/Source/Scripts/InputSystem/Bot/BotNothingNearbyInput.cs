using System;
using System.Collections.Generic;
using Extensions;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace InputSystem.Bot
{
    public class BotNothingNearbyInput : BotRandomInput<NothingNearbyState>
    {
        private readonly IDisposable _subscription;
        private readonly Dictionary<int, BotAction> _directions;
        private readonly BotMovement _botMovement;

        private Type _lastState;

        public BotNothingNearbyInput(
            IStateMachine stateMachine, BotMovement botMovement, BotAction left, BotAction right, BotAction inPlace)
            : base(stateMachine, left, right, inPlace)
        {
            _directions = new Dictionary<int, BotAction>(3)
            {
                { Directions.Left, left },
                { Directions.Right, right },
                { Directions.InPlace, inPlace }
            };

            _subscription = stateMachine.CurrentState
                .Subscribe(state => _lastState = state.Type);
            
            _botMovement = botMovement;
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

            if (_lastState == typeof(OpponentNearbyState) && _botMovement.Direction == Directions.Left)
                direction = Directions.Left;

            if (_lastState == typeof(WallNearbyState) && _botMovement.Direction == Directions.Right)
                direction = Directions.Right;
                
            return _directions[direction];
        }
    }
}