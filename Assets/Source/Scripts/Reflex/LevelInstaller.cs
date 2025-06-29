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
        { // Может быть стоит убрать подобное заполнение и автоматически добавлять новый стейт, если такого не было ранее
            IState[] states =
            {
                new IdleState(),
                new MoveLeftState(),
                new MoveRightState(),
                new JumpState(),
                new MoveJumpState(),
                new PunchState(),
                new KickState()
            };

            var conditionBuilder = new ConditionBuilder();

            conditionBuilder.Add(ConditionType.Stay, _ => _inputReader.Direction.CurrentValue == 0);
            conditionBuilder.Add(ConditionType.MoveLeft, _ => _inputReader.Direction.CurrentValue < 0);
            conditionBuilder.Add(ConditionType.MoveRight, _ => _inputReader.Direction.CurrentValue > 0);
            conditionBuilder.Add(ConditionType.Jump, _ => _jump.IsExecuting);
            conditionBuilder.Add(ConditionType.Attack, _ => _attacker.IsExecuting);

            Func<Unit, bool> idleCondition = conditionBuilder.Build(
                (ConditionType.Stay, true),
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> moveLeftCondition = conditionBuilder.Build(
                (ConditionType.MoveLeft, true),
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> moveRightCondition = conditionBuilder.Build(
                (ConditionType.MoveRight, true),
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> moveJumpCondition = conditionBuilder.Build(
                (ConditionType.Stay, false),
                (ConditionType.Jump, true),
                (ConditionType.Attack, false));
            Func<Unit, bool> jumpCondition = conditionBuilder.Build(
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> attackCondition = conditionBuilder.Build((ConditionType.Attack, false));

            var stateMachine = new CharacterStateMachine(states, states[0]);

            var transitionInitializer = new TransitionInitializer(stateMachine)
                .InitializeTransition<IdleState, float>(_inputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(_inputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(_inputReader.Direction, moveRightCondition)
                .InitializeTransition<JumpState, Unit>(_inputReader.JumpPressed, jumpCondition)
                .InitializeTransition<MoveJumpState, float>(_inputReader.Direction, moveJumpCondition)
                .InitializeTransition<PunchState, Unit>(_inputReader.PunchPressed, attackCondition)
                .InitializeTransition<KickState, Unit>(_inputReader.KickPressed, attackCondition);

            builder.AddSingleton(stateMachine, typeof(IStateChangeable));
        }
    }
}