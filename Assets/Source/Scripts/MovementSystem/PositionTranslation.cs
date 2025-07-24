using Extensions;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace MovementSystem
{
    public abstract class PositionTranslation : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private DistanceValidator _distanceValidator;

        private Transform _transform;
        private IStateMachine _stateMachine;
        private IDirectionChangeable _directionChangeable;

        private void Start()
        {
            _transform = transform;
            _directionChangeable.Direction
                .Where(_ => _stateMachine.CurrentState.CurrentValue is MoveState)
                .Subscribe(TranslatePosition)
                .AddTo(this);
        }

        protected void Initialize(IStateMachine stateMachine, IConditionAddable conditionAddable,
            IDirectionChangeable directionChangeable)
        {
            _directionChangeable = directionChangeable;
            _stateMachine = stateMachine;

            conditionAddable.Add<IdleState>(_ => _directionChangeable.Direction.CurrentValue == 0);
            conditionAddable.Add<MoveRightState>(_ => _directionChangeable.Direction.CurrentValue > 0);
            conditionAddable.Add<MoveLeftState>(_ => _directionChangeable.Direction.CurrentValue < 0);
        }

        private void TranslatePosition(float direction)
        {
            float targetDistance = direction * _moveSpeed * Time.deltaTime;
            Vector3 targetPosition = _transform.position + Vector3.right * targetDistance;

            if (_distanceValidator.IsValidDistance(targetPosition) == false)
                return;

            _transform.position = targetPosition;
        }
    }
}