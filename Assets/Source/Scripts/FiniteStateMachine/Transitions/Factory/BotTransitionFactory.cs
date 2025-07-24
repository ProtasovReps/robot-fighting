using System;
using FiniteStateMachine.States;
using InputSystem;
using R3;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotTransitionFactory : StateTransitionFactory
    {
        [SerializeField] private BotInputReader _botInputReader;
        
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine)
        {
            Func<Unit, bool> idleCondition = builder.Build(
                (typeof(IdleState), true),
                (typeof(HittedState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveLeftCondition = builder.Build(
                (typeof(MoveLeftState), true),
                (typeof(HittedState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveRightCondition = builder.Build(
                (typeof(MoveRightState), true),
                (typeof(HittedState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> hittedCondition = builder.Build((typeof(HittedState), true));
            Func<Unit, bool> attackCondition = builder.Build((typeof(AttackState), false));
        
            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, float>(_botInputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(_botInputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(_botInputReader.Direction, moveRightCondition)
                .InitializeTransition<HittedState, float>(_botInputReader.Direction, hittedCondition) // Не должно из InputReader
                .InitializeTransition<PunchState, Unit>(_botInputReader.UpAttackPressed, attackCondition)
                .InitializeTransition<KickState, Unit>(_botInputReader.DownAttackPressed, attackCondition);
        }
    }
}