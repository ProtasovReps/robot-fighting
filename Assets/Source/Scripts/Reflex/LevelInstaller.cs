using System;
using Extensions;
using FightingSystem;
using FiniteStateMachine;
using FiniteStateMachine.States;
using FiniteStateMachine.Conditions;
using InputSystem;
using Interface;
using MovementSystem;
using R3;
using Reflex.Core;
using UnityEngine;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Jump _jump; // мок
        [SerializeField] private Attacker _attacker;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallInput(containerBuilder);
            InstallStateMachine(containerBuilder);
        }

        private void InstallInput(ContainerBuilder builder)
        {
            UserInput input = new();

            _inputReader.Initialize(input);
            builder.AddSingleton(_inputReader);
        }

        private void InstallStateMachine(ContainerBuilder builder)
        {
            IState[] states =
            {
                new IdleState(),
                new MoveState(),
                new JumpState(),
                new MoveJumpState(),
                new ArmAttackState()
            };

            var conditionBuilder = new ConditionBuilder();

            conditionBuilder.Add(ConditionType.Move, _ => _inputReader.Direction.CurrentValue != 0);
            conditionBuilder.Add(ConditionType.Jump, _ => _jump.IsExecuting);
            conditionBuilder.Add(ConditionType.ArmAttack, _ => _attacker.IsExecuting);

            Func<Unit, bool> stayCondition = conditionBuilder.Build(
                (ConditionType.Move, false),
                (ConditionType.Jump, false),
                (ConditionType.ArmAttack, false));
            Func<Unit, bool> moveCondition = conditionBuilder.Build(
                (ConditionType.Move, true),
                (ConditionType.Jump, false),
                (ConditionType.ArmAttack, false));
            Func<Unit, bool> moveJumpCondition = conditionBuilder.Build(
                (ConditionType.Move, true),
                (ConditionType.Jump, true),
                (ConditionType.ArmAttack, false));
            Func<Unit, bool> jumpCondition = conditionBuilder.Build(
                (ConditionType.Jump, false),
                (ConditionType.ArmAttack, false));
            Func<Unit, bool> attackCondition = conditionBuilder.Build((ConditionType.ArmAttack, false));

            var stateMachine = new CharacterStateMachine(states, states[0]);

            var transitionInitializer = new TransitionInitializer(stateMachine)
                .InitializeTransition<IdleState, float>(_inputReader.Direction, stayCondition)
                .InitializeTransition<MoveState, float>(_inputReader.Direction, moveCondition)
                .InitializeTransition<JumpState, Unit>(_inputReader.JumpPressed, jumpCondition)
                .InitializeTransition<MoveJumpState, float>(_inputReader.Direction, moveJumpCondition)
                .InitializeTransition<ArmAttackState, Unit>(_inputReader.PunchPressed, attackCondition);

            builder.AddSingleton(stateMachine, typeof(IStateChangeable));
        }
    }
}