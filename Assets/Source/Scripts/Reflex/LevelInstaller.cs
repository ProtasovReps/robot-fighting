using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Factory;
using FiniteStateMachine.Transitions.Factory;
using HealthSystem;
using InputSystem;
using Interface;
using Reflex.Core;
using UnityEngine;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private BotData _botData;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallInput();
            InstallFighters(containerBuilder);
        }

        private void InstallInput()
        {
            UserInput input = new();
            BotMovementInput botMovementInput = new(_botData.ChangeDirectionInterval);
            BotAttackInput botAttackInput = new(_botData.AttackDelay);

            _playerData.PlayerInputReader.Initialize(input);
            _botData.BotInputReader.Initialize(botMovementInput, botAttackInput);
            _playerData.PositionTranslation.Initialize(_playerData.PlayerInputReader);
            _botData.PositionTranslation.Initialize(_botData.BotInputReader);
        }

        private void InstallFighters(ContainerBuilder builder)
        {
            AnimationFactory animationFactory = new();
            CharacterStateMachine playerStateMachine = new PlayerStateMachineFactory().Produce();
            CharacterStateMachine botStateMachine = new BotStateMachineFactory().Produce();

            PlayerData playerData = new PlayerFactory().Produce(_playerData, animationFactory, playerStateMachine);
            BotData botData = new BotFactory().Produce(_botData, animationFactory, botStateMachine);

            new PlayerTransitionFactory(playerData).InstallMachine(playerStateMachine);
            new BotTransitionFactory(botData).InstallMachine(botStateMachine);

            builder.AddSingleton(playerStateMachine, typeof(IPlayerStateMachine));
            builder.AddSingleton(botStateMachine, typeof(IBotStateMachine));
        }
    }
}