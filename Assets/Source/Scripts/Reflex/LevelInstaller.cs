using CharacterSystem.Data;
using CharacterSystem.Factory;
using FiniteStateMachine.Factory;
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

            _playerData.PlayerInputReader.Initialize(input);
            _botData.BotInputReader.Initialize(botMovementInput);
            _playerData.PositionTranslation.Initialize(_playerData.PlayerInputReader);
            _botData.PositionTranslation.Initialize(_botData.BotInputReader);
        }

        private void InstallFighters(ContainerBuilder builder)
        {
            PlayerStateMachineFactory playerStateMachineFactory =
                new(_playerData.PlayerInputReader, _playerData.Jump, _playerData.Attacker);
            BotStateMachineFactory botStateMachineFactory = new(_botData.BotInputReader);

            IPlayerStateMachine playerStateMachine = playerStateMachineFactory.Produce();
            IBotStateMachine botStateMachine = botStateMachineFactory.Produce();

            PlayerFactory playerFactory = new(_playerData, playerStateMachine);
            BotFactory botFactory = new(_botData, botStateMachine);

            playerFactory.Produce();
            botFactory.Produce();

            builder.AddSingleton(playerStateMachine, typeof(IPlayerStateMachine));
            builder.AddSingleton(botStateMachine, typeof(IBotStateMachine));
        }
    }
}