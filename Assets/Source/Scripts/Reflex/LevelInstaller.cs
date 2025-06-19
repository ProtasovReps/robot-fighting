using FiniteStateMachine;
using FiniteStateMachine.States;
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

            var stateMachine = new CharacterStateMachine(states, states[0]);
            var initializer = new TransitionInitializer(stateMachine)
                .InitializeTransition<IdleState, float>(
                    _inputReader.Direction
                        .Where(direction => direction == 0 && _jump.IsGrounded))
                .InitializeTransition<MoveState, float>(
                    _inputReader.Direction
                        .Where(direction => direction != 0 && _jump.IsGrounded))
                .InitializeTransition<MoveJumpState, float>(
                    _inputReader.Direction
                        .Where(direction => direction != 0 && _jump.IsGrounded == false))
                .InitializeTransition<JumpState, Unit>(
                    _inputReader.JumpPressed
                        .Where(_ => _jump.IsGrounded))
                .InitializeTransition<ArmAttackState, Unit>(
                    _inputReader.PunchPressed);
            
            
            builder.AddSingleton(stateMachine, typeof(IStateChangeable));
        }
    }
}