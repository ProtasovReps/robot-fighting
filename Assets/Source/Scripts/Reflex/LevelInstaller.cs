using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.Factory;
using FiniteStateMachine.States;
using FiniteStateMachine.Transitions.Factory;
using InputSystem;
using InputSystem.Bot;
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
            State[] states = new PlayerStateFactory().Produce();
            PlayerStateMachine playerStateMachine = new(states);
            PlayerConditionBuilder conditionBuilder = new();
            
            _playerFactory.Produce(animationFactory, playerStateMachine, conditionBuilder);
            _playerTransitionFactory.Initialize(playerStateMachine, conditionBuilder);
            
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(playerStateMachine);
        }

        private BotData InstallBot(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            State[] states = new BotStateFactory().Produce();
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
            State[] states = new BotInputStateFactory().Produce();
            BotInputStateMachine stateMachine = new(states);
            BotInputConditionBuilder conditionBuilder = new();
            
            BotMovement botMovement = new();
            BotAttack botAttack = new();
            // это вынести в фабрику
            BotAction leftMove = new(botMovement.MoveLeft, botData.MoveDuration);
            BotAction rightMove = new(botMovement.MoveRight, botData.MoveDuration);
            BotAction inPlace = new(botMovement.Stop, botData.MoveDuration);
            BotAction upAttack = new(botAttack.AttackUp, botData.AttackDelay);
            BotAction downAttack = new(botAttack.AttackDown, botData.AttackDelay);
            
            new BotNothingNearbyInput(stateMachine, botMovement, leftMove, rightMove, inPlace);
            new BotSoloInput<WallNearbyState>(stateMachine, rightMove);
            new BotRandomInput<OpponentNearbyState>(stateMachine, leftMove, upAttack, downAttack);
            new BotRandomInput<WallOpponentNearbyState>(stateMachine, upAttack, downAttack);
            
            _botInputReader.Initialize(botMovement, botAttack);
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