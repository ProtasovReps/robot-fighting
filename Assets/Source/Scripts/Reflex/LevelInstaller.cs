using System.Collections.Generic;
using AnimationSystem;
using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.Factory;
using FiniteStateMachine.States;
using FiniteStateMachine.Transitions.Factory;
using InputSystem;
using InputSystem.Bot;
using Interface;
using MovementSystem;
using Reflex.Core;
using UnityEngine;
using BotMovement = MovementSystem.BotMovement;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [Header("Player")]
        [SerializeField] private PlayerTransitionFactory _playerTransitionFactory;
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private DirectionValidationFactory _playerDirectionValidationFactory;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private AnimatedCharacter _playerAnimatedCharacter;
        [Header("Bot")]
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
        [SerializeField] private BotInputTransitionFactory _botInputTransitionFactory;
        [SerializeField] private BotFactory _botFactory;
        [SerializeField] private BotMovement _botMovement;
        [SerializeField] private DirectionValidationFactory _botDirectionValidationFactory;
        [SerializeField] private AnimatedCharacter _botAnimatedCharacter;
        
        private void Start()
        {
            Destroy(gameObject);
        }

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            AnimationFactory animationFactory = new();
            
            InstallBot(animationFactory, containerBuilder);
            InstallPlayer(animationFactory, containerBuilder);
        }

        private void InstallPlayer(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            State[] states = new PlayerStateFactory().Produce();
            PlayerStateMachine playerStateMachine = new(states);
            PlayerConditionBuilder conditionBuilder = new();
            PlayerData playerData = _playerFactory.Produce(playerStateMachine, conditionBuilder);
            IMoveInput moveInput = InstallPlayerInput(playerData, builder);
            
            _playerTransitionFactory.Initialize(playerStateMachine, conditionBuilder);
            InstallPlayerMovement(playerData, moveInput);
            InstallAnimations(animationFactory, _playerAnimatedCharacter, playerStateMachine);
            
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(playerStateMachine);
        }

        private void InstallBot(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            State[] states = new BotStateFactory().Produce();
            BotStateMachine botStateMachine = new(states);
            BotConditionBuilder conditionBuilder = new();
            BotData botData = _botFactory.Produce(botStateMachine, conditionBuilder);
            IMoveInput moveInput = InstallBotInput(botData, builder);

            _botTransitionFactory.Initialize(botStateMachine, conditionBuilder);
            InstallBotMovement(botData, moveInput);
            InstallAnimations(animationFactory, _botAnimatedCharacter, botStateMachine);
            
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(botStateMachine);
        }

        private IMoveInput InstallBotInput(BotData botData, ContainerBuilder builder)
        {
            State[] states = new BotInputStateFactory().Produce();
            BotInputStateMachine stateMachine = new(states);
            BotInputConditionBuilder conditionBuilder = new();
            
            BotMoveInput botMoveInput = new();
            BotAttackInput botAttackInput = new();
            Dictionary<int, DistanceValidator> directions = _botDirectionValidationFactory.Produce();
            ValidatedInput validatedBotInput = new(botData.transform, botMoveInput, directions);
            
            _botInputTransitionFactory.Initialize(stateMachine, conditionBuilder);
            
            InstallBotActions(botMoveInput, botAttackInput, botData, stateMachine);

            builder.AddSingleton(botMoveInput);
            builder.AddSingleton(botAttackInput);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(stateMachine);
            return validatedBotInput;
        }

        private void InstallBotActions(
            BotMoveInput moveInput,
            BotAttackInput attackInput, 
            BotData botData,
            BotInputStateMachine stateMachine)
        {  
            BotAction leftMove = new(moveInput.MoveLeft, botData.MoveDuration);
            BotAction rightMove = new(moveInput.MoveRight, botData.MoveDuration);
            BotAction inPlace = new(moveInput.Stop, botData.MoveDuration);
            BotAction upAttack = new(attackInput.AttackUp, botData.AttackDelay);
            BotAction downAttack = new(attackInput.AttackDown, botData.AttackDelay);
            
            new BotNothingNearbyActionExecutor(stateMachine, moveInput, leftMove, rightMove, inPlace);
            new BotSoloActionExecutor<WallNearbyState>(stateMachine, rightMove);
            new BotRandomActionExecutor<OpponentNearbyState>(stateMachine, leftMove, upAttack, downAttack);
            new BotRandomActionExecutor<WallOpponentNearbyState>(stateMachine, upAttack, downAttack);
        }

        private IMoveInput InstallPlayerInput(PlayerData playerData, ContainerBuilder builder)
        {
            UserInput input = new();
            PlayerMoveInputReader moveInputReader = new(input);
            PlayerAttackInputReader attackInputReader = new(input);
            Dictionary<int, DistanceValidator> directions = _playerDirectionValidationFactory.Produce();
            ValidatedInput validatedInput = new(playerData.transform, moveInputReader, directions);
            
            input.Enable(); // управление инпутом в другое место
            builder.AddSingleton(moveInputReader);
            builder.AddSingleton(attackInputReader);
            return validatedInput;
        }

        private void InstallPlayerMovement(PlayerData fighterData, IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(fighterData.transform, fighterData.MoveSpeed);
            _playerMovement.Initialize(moveInput, positionTranslation);
        }
        
        private void InstallBotMovement(BotData fighterData, IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(fighterData.transform, fighterData.MoveSpeed);
            _botMovement.Initialize(moveInput, positionTranslation);
        }
        
        private void InstallAnimations(
            AnimationFactory animationFactory, 
            AnimatedCharacter animatedCharacter, 
            IStateMachine stateMachine)
        {
            animationFactory.Produce(animatedCharacter, stateMachine);
        }
    }
}