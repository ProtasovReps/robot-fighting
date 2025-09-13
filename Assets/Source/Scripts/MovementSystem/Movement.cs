using System;
using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class Movement<TStateMachine, TConditionBuilder> : MonoBehaviour
        where TStateMachine : IStateMachine
        where TConditionBuilder : IConditionAddable
    {
        private IMoveInput _moveInput;
        private IStateMachine _stateMachine;
        private PositionTranslation _positionTranslation;

        [Inject]
        private void Inject(TStateMachine stateMachine, TConditionBuilder conditionBuilder)
        {
            _stateMachine = stateMachine;

            conditionBuilder.Add<IdleState>(_ => _moveInput.Value.CurrentValue == 0);
            conditionBuilder.Add<MoveRightState>(_ => _moveInput.Value.CurrentValue > 0);
            conditionBuilder.Add<MoveLeftState>(_ => _moveInput.Value.CurrentValue < 0);
        }

        private void Start()
        {
            _moveInput.Value
                .Where(_ => _stateMachine.Value.CurrentValue is MoveState)
                .Subscribe(_positionTranslation.TranslatePosition)
                .AddTo(this);

            _stateMachine.Value
                .Where(value => value.Type == typeof(IdleState))
                .Subscribe(_ => _positionTranslation.ResetSpeed())
                .AddTo(this);
        }

        public void Initialize(IMoveInput moveInput, PositionTranslation positionTranslation)
        {
            if (moveInput == null)
                throw new ArgumentNullException(nameof(moveInput));

            if (positionTranslation == null)
                throw new ArgumentNullException(nameof(positionTranslation));

            _moveInput = moveInput;
            _positionTranslation = positionTranslation;
        }
    }
}