using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
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
        [Header("Player")]
        [SerializeField] private PlayerTransitionFactory _playerTransitionFactory;
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private PlayerInputReader _playerInputReader;
        [Header("Bot")]
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
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
            InstallInput(botData);
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

        private void InstallBotInput(ContainerBuilder builder)
        {
            ConditionBuilder conditionBuilder = new();
            // builder.AddSingleton(conditionBuilder, )
        }
        
        private void InstallInput(BotData botData)
        {
            UserInput input = new();
            BotMovementInput botMovementInput = new(botData.ChangeDirectionInterval);
            BotAttackInput botAttackInput = new(botData.AttackDelay);

            _playerInputReader.Initialize(input);
            _botInputReader.Initialize(botMovementInput, botAttackInput);
        }
    }
}