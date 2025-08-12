using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using Reflex.Attributes;
using UnityEngine;

namespace Extensions
{
    public class DistanceValidationInitializer : MonoBehaviour // возмонжо уберется
    {
        [SerializeField] private DistanceValidator _opponentDistanceValidator;
        [SerializeField] private DistanceValidator _wallDistanceValidator;
        [SerializeField] private Transform _transform;

        [Inject]
        private void Inject(BotInputConditionBuilder conditionBuilder)
        {
            conditionBuilder.Add<OpponentNearbyState>(_ => _opponentDistanceValidator.IsValidDistance(_transform.position) == false);
            conditionBuilder.Add<WallNearbyState>(_ => _wallDistanceValidator.IsValidDistance(_transform.position) == false);
        }
    }
}