using FiniteStateMachine;
using FiniteStateMachine.States;
using FiniteStateMachine.TransitionConditions;
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
                .InitializeTransition<IdleState, float>(new IdleCondition(_inputReader, _jump).GetCondition())
                .InitializeTransition<MoveState, float>(new MoveCondition(_inputReader, _jump).GetCondition())
                .InitializeTransition<MoveJumpState, float>(new MoveJumpCondition(_inputReader, _jump).GetCondition())
                .InitializeTransition<JumpState, Unit>(new JumpCondition(_inputReader, _jump).GetCondition())
                .InitializeTransition<ArmAttackState, Unit>(new PunchCondition(_inputReader).GetCondition());
            
            builder.AddSingleton(stateMachine, typeof(IStateChangeable));
        }
    }
}