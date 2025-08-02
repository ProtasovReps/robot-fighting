using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.Factory;
using FiniteStateMachine.States;
using FiniteStateMachine.Transitions.Factory;
using InputSystem;
using Interface;
using Reflex.Core;
using UnityEngine;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [Header("Player")]
        [SerializeField] private PlayerTransitionFactory _playerTransitionFactory;
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private PlayerInputReader _playerInputReader;
        [Header("Bot")]
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
        [SerializeField] private BotInputTransitionFactory _botInputTransitionFactory;
        [SerializeField] private BotFactory _botFactory;
        [SerializeField] private BotInputReader _botInputReader;

        private void Start()
        {
            Destroy(gameObject);
        }

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            AnimationFactory animationFactory = new();
            BotData botData = InstallBot(animationFactory, containerBuilder);
            
            InstallPlayer(animationFactory, containerBuilder);
            InstallBotInput(botData, containerBuilder);
            InstallPlayerInput();
        }

        private void InstallPlayer(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            IState[] states = new PlayerStateFactory().Produce();
            PlayerStateMachine playerStateMachine = new(states);
            PlayerConditionBuilder conditionBuilder = new();
            
            _playerFactory.Produce(animationFactory, playerStateMachine, conditionBuilder);
            _playerTransitionFactory.Initialize(playerStateMachine, conditionBuilder);
            
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(playerStateMachine);
        }

        private BotData InstallBot(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            IState[] states = new BotStateFactory().Produce();
            BotStateMachine botStateMachine = new(states);
            BotConditionBuilder conditionBuilder = new();
            BotData botData = _botFactory.Produce(animationFactory, botStateMachine, conditionBuilder);

            _botTransitionFactory.Initialize(botStateMachine, conditionBuilder);
            
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(botStateMachine);
            return botData;
        }

        private void InstallBotInput(BotData botData, ContainerBuilder builder)
        {
            IState[] states = new BotInputStateFactory().Produce();
            BotInputStateMachine stateMachine = new(states);
            BotInputConditionBuilder conditionBuilder = new();
            BotMovementInput botMovementInput = new(botData.ChangeDirectionInterval, stateMachine);
            BotAttackInput botAttackInput = new(botData.AttackDelay);

            new BotInputPicker<NothingNearbyState>(stateMachine, botMovementInput);
            new BotInputPicker<WallNearbyState>(stateMachine, botMovementInput);
            new BotInputPicker<OpponentNearbyState>(stateMachine, botMovementInput, botAttackInput);
            new BotInputPicker<WallOpponentNearbyState>(stateMachine, botAttackInput);
            
            _botInputReader.Initialize(botMovementInput, botAttackInput);
            _botInputTransitionFactory.Initialize(stateMachine, conditionBuilder);
            
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(stateMachine);
        }
        
        private void InstallPlayerInput()
        {
            UserInput input = new();
            
            _playerInputReader.Initialize(input);
        }
    }
}