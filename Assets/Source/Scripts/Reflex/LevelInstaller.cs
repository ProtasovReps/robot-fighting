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

            var idleCondition = new MoveCondition(_inputReader, false, new JumpCondition(_jump, false, new SourceCondition<float>(_inputReader.Direction)));
            var moveCondition = new MoveCondition(_inputReader, true, new JumpCondition(_jump, false, new SourceCondition<float>(_inputReader.Direction)));
            var jumpCondition = new JumpCondition(_jump, false, new SourceCondition<Unit>(_inputReader.JumpPressed));
            var moveJumpCondition = new MoveCondition(_inputReader, true, new JumpCondition(_jump, true, new SourceCondition<float>(_inputReader.Direction)));
            
            var stateMachine = new CharacterStateMachine(states, states[0]);
            var initializer = new TransitionInitializer(stateMachine)
                .InitializeTransition<IdleState, Unit>(idleCondition.GetCondition())
                .InitializeTransition<MoveState, Unit>(moveCondition.GetCondition())
                .InitializeTransition<MoveJumpState, Unit>(moveJumpCondition.GetCondition())
                .InitializeTransition<JumpState, Unit>(jumpCondition.GetCondition());

            builder.AddSingleton(stateMachine, typeof(IStateChangeable));
        }
    }
}