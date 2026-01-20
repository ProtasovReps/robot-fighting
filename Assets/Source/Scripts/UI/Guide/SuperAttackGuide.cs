using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace UI.Guide
{
    public class SuperAttackGuide : StateDependentGuide<SuperAttackState>
    {
        [SerializeField] private DistanceValidator _distanceValidator;
        [SerializeField] private Transform _player;

        [Inject]
        private void Inject(PlayerStateMachine playerStateMachine)
        {
            playerStateMachine.Value
                .Where(state => state is AttackState)
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }

        protected override bool IsValidCondition()
        {
            return _distanceValidator.IsValidDistance(_player.position) == false;
        }
    }
}