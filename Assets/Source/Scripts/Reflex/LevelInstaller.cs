using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Factory;
using FiniteStateMachine.Transitions.Factory;
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
        [SerializeField] private PlayerTransitionFactory _playerTransitionFactory;
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
        
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
            _playerData.PositionTranslation.SetInput(_playerData.PlayerInputReader);
            _botData.PositionTranslation.SetInput(_botData.BotInputReader);
        }

        private void InstallFighters(ContainerBuilder builder)
        {
            AnimationFactory animationFactory = new();

            InstallPlayer(animationFactory, builder);
            InstallBot(animationFactory, builder);
        }

        private void InstallPlayer(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            CharacterStateMachine playerStateMachine = new PlayerStateMachineFactory().Produce();
            ConditionBuilder conditionBuilder = new();

            new PlayerFactory().Produce(_playerData, animationFactory, playerStateMachine, conditionBuilder);
            _playerTransitionFactory.Initialize(playerStateMachine, conditionBuilder);

            builder.AddSingleton(conditionBuilder, typeof(IPlayerConditionAddable));
            builder.AddSingleton(playerStateMachine, typeof(IPlayerStateMachine));
        }

        private void InstallBot(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            CharacterStateMachine botStateMachine = new BotStateMachineFactory().Produce();
            ConditionBuilder conditionBuilder = new();

            new BotFactory().Produce(_botData, animationFactory, botStateMachine, conditionBuilder);
            _botTransitionFactory.Initialize(botStateMachine, conditionBuilder);
            
            builder.AddSingleton(conditionBuilder, typeof(IBotConditionAddable));
            builder.AddSingleton(botStateMachine, typeof(IBotStateMachine));
        }
    }
}