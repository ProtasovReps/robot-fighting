using System.Collections.Generic;
using AnimationSystem;
using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.Factory;
using DamageCalculationSystem;
using Extensions;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.Factory;
using FiniteStateMachine.States;
using FiniteStateMachine.Transitions.Factory;
using HealthSystem;
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
        [SerializeField] private PlayerAttackFactory _playerAttackFactory;
        [SerializeField] private HitFactory _playerHitFactory;
        [SerializeField] private DirectionValidationFactory _playerDirectionValidationFactory;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private AnimatedCharacter _playerAnimatedCharacter;
        [SerializeField] private PlayerData _playerData;
        [Header("Bot")]
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
        [SerializeField] private BotAttackFactory _botAttackFactory;
        [SerializeField] private HitFactory _botHitFactory;
        [SerializeField] private BotInputTransitionFactory _botInputTransitionFactory;
        [SerializeField] private BotMovement _botMovement;
        [SerializeField] private DirectionValidationFactory _botDirectionValidationFactory;
        [SerializeField] private AnimatedCharacter _botAnimatedCharacter;
        [SerializeField] private BotData _botData;
        
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
            PlayerHealth health = new(_playerData.StartHealthValue);
            IMoveInput moveInput = InstallPlayerInput(builder);
            
             _playerAttackFactory.Produce(_playerData);
            _playerHitFactory.Produce(health, playerStateMachine, conditionBuilder);
            _playerTransitionFactory.Initialize(playerStateMachine, conditionBuilder);
            
            PositionTranslation positionTranslation = InstallPlayerMovement(moveInput);
            
            animationFactory.Produce(_playerAnimatedCharacter, playerStateMachine, _playerData, positionTranslation);
            
            builder.AddSingleton(health);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(playerStateMachine);
        }

        private void InstallBot(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            State[] states = new BotStateFactory().Produce();
            BotStateMachine botStateMachine = new(states);
            BotConditionBuilder conditionBuilder = new();
            BotHealth health = new(_botData.StartHealthValue);
            IMoveInput moveInput = InstallBotInput(builder);

            _botAttackFactory.Produce(_botData);
            _botHitFactory.Produce(health, botStateMachine, conditionBuilder);
            _botTransitionFactory.Initialize(botStateMachine, conditionBuilder);
            
            PositionTranslation positionTranslation = InstallBotMovement(moveInput);
            
            animationFactory.Produce(_botAnimatedCharacter, botStateMachine, _botData, positionTranslation);

            builder.AddSingleton(health);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(botStateMachine);
        }

        private IMoveInput InstallBotInput(ContainerBuilder builder)
        {
            State[] states = new BotInputStateFactory().Produce();
            BotInputStateMachine stateMachine = new(states);
            BotInputConditionBuilder conditionBuilder = new();
            
            BotMoveInput botMoveInput = new();
            BotAttackInput botAttackInput = new();
            Dictionary<int, DistanceValidator> directions = _botDirectionValidationFactory.Produce();
            ValidatedInput validatedBotInput = new(_botData.transform, botMoveInput, directions);
            
            _botInputTransitionFactory.Initialize(stateMachine, conditionBuilder);
            
            InstallBotActions(botMoveInput, botAttackInput, stateMachine);

            builder.AddSingleton(botMoveInput);
            builder.AddSingleton(botAttackInput);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(stateMachine);
            return validatedBotInput;
        }

        private void InstallBotActions(
            BotMoveInput moveInput,
            BotAttackInput attackInput, 
            BotInputStateMachine stateMachine)
        {  
            BotAction leftMove = new(moveInput.MoveLeft, _botData.MoveDuration);
            BotAction rightMove = new(moveInput.MoveRight, _botData.MoveDuration);
            BotAction inPlace = new(moveInput.Stop, _botData.MoveDuration);
            BotAction upAttack = new(attackInput.AttackUp, _botData.AttackDelay);
            BotAction downAttack = new(attackInput.AttackDown, _botData.AttackDelay);
            
            new BotNothingNearbyActionExecutor(stateMachine, moveInput, leftMove, rightMove, inPlace);
            new BotSoloActionExecutor<WallNearbyState>(stateMachine, rightMove);
            new BotRandomActionExecutor<OpponentNearbyState>(stateMachine, leftMove, upAttack, downAttack);
            new BotRandomActionExecutor<WallOpponentNearbyState>(stateMachine, upAttack, downAttack);
        }

        private IMoveInput InstallPlayerInput(ContainerBuilder builder)
        {
            UserInput input = new();
            PlayerMoveInputReader moveInputReader = new(input);
            PlayerAttackInputReader attackInputReader = new(input);
            Dictionary<int, DistanceValidator> directions = _playerDirectionValidationFactory.Produce();
            ValidatedInput validatedInput = new(_playerData.transform, moveInputReader, directions);
            
            input.Enable(); // управление инпутом в другое место
            builder.AddSingleton(moveInputReader);
            builder.AddSingleton(attackInputReader);
            return validatedInput;
        }

        private PositionTranslation InstallPlayerMovement(IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(_playerData.transform, _playerData.MoveSpeed);
            _playerMovement.Initialize(moveInput, positionTranslation);
            return positionTranslation;
        }
        
        private PositionTranslation InstallBotMovement(IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(_botData.transform, _botData.MoveSpeed);
            _botMovement.Initialize(moveInput, positionTranslation);
            return positionTranslation;
        }
    }
}