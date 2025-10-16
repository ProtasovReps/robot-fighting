using System.Collections.Generic;
using AnimationSystem;
using AnimationSystem.Factory;
using CharacterSystem;
using CharacterSystem.Parameters;
using HitSystem;
using Extensions;
using FightingSystem;
using FightingSystem.Dying;
using FightingSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.Factory;
using FiniteStateMachine.Transitions.Factory;
using HealthSystem;
using ImplantSystem;
using ImplantSystem.Factory;
using InputSystem;
using InputSystem.Bot;
using InputSystem.Bot.Factory;
using Interface;
using MovementSystem;
using Reflex.Core;
using UnityEngine;
using BotMovement = MovementSystem.BotMovement;
using State = FiniteStateMachine.States.State;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [Header("Player")]
        [SerializeField] private PlayerTransitionFactory _playerTransitionFactory;
        [SerializeField] private PlayerAttackFactory _playerAttackFactory;
        [SerializeField] private PlayerHitFactory _playerHitFactory; // поменять на конкретный тип
        [SerializeField] private DirectionValidationFactory _playerDirectionValidationFactory;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerParameters _playerParameters;
        [SerializeField] private PlayerImplantFactory _playerImplantFactory;
        [SerializeField] private Animator _playerAnimator; // убрать с приходом сейвов
        [SerializeField] private AnimatedCharacter _playerAnimatedCharacter; // убрать с приходом сейвов
        
        [Header("Bot")]
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
        [SerializeField] private BotAttackFactory _botAttackFactory;
        [SerializeField] private BotHitFactory _botHitFactory; // поменять на конкретный тип
        [SerializeField] private BotInputTransitionFactory _botInputTransitionFactory;
        [SerializeField] private BotMovement _botMovement;
        [SerializeField] private DirectionValidationFactory _botDirectionValidationFactory;
        [SerializeField] private BotParameters _botParameters;
        [SerializeField] private BotImplantFactory _botImplantFactory;
        [SerializeField] private ActionFactory _botActionFactory;
        [SerializeField] private Animator _botAnimator;
        [SerializeField] private AnimatedCharacter _botAnimatedCharacter;
        
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
            PlayerHealth health = new(_playerParameters.StartHealthValue);
            IMoveInput moveInput = InstallPlayerInput(builder);
            ImplantPlaceHolderStash placeHolderStash = _playerImplantFactory.Produce();
            HitReader hitReader = _playerHitFactory.Produce(health, playerStateMachine, conditionBuilder);
            AttackAnimationOverrider animationOverrider = new(_playerAnimator);

            new Stretch(playerStateMachine, conditionBuilder);
            
            _playerAttackFactory.Produce(placeHolderStash);
            _playerTransitionFactory.Initialize(playerStateMachine, conditionBuilder);
            
            PositionTranslation positionTranslation = InstallPlayerMovement(moveInput);
            
            animationFactory.Produce(_playerAnimatedCharacter, _playerAnimator, playerStateMachine, _playerParameters, positionTranslation);
            animationOverrider.Override(placeHolderStash);
           
            builder.AddSingleton(new PlayerDeath(hitReader, health, conditionBuilder));
            builder.AddSingleton(new SuperAttackCharge(hitReader, playerStateMachine, conditionBuilder));
            builder.AddSingleton(health);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(playerStateMachine);
        }

        private void InstallBot(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            State[] states = new BotStateFactory().Produce();
            BotStateMachine botStateMachine = new(states);
            BotConditionBuilder conditionBuilder = new();
            BotHealth health = new(_botParameters.StartHealthValue);
            IMoveInput moveInput = InstallBotInput(builder);
            ImplantPlaceHolderStash placeHolderStash = _botImplantFactory.Produce();
            HitReader hitReader = _botHitFactory.Produce(health, botStateMachine, conditionBuilder);
            AttackAnimationOverrider animationOverrider = new(_botAnimator);

            _botAttackFactory.Produce(placeHolderStash);
            _botTransitionFactory.Initialize(botStateMachine, conditionBuilder);
            
            PositionTranslation positionTranslation = InstallBotMovement(moveInput);
            
            animationFactory.Produce(_botAnimatedCharacter, _botAnimator, botStateMachine, _botParameters, positionTranslation);
            animationOverrider.Override(placeHolderStash);

            builder.AddSingleton(new BotDeath(hitReader, health, conditionBuilder));
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
            BotFightInput botFightInput = new();
            Dictionary<int, DistanceValidator> directions = _botDirectionValidationFactory.Produce();
            ValidatedInput validatedBotInput = new(_botParameters.transform, botMoveInput, directions);
            
            _botInputTransitionFactory.Initialize(stateMachine, conditionBuilder);
            
            InstallBotActions(botMoveInput, botFightInput, stateMachine);

            builder.AddSingleton(validatedBotInput);
            builder.AddSingleton(botFightInput);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(stateMachine);
            return validatedBotInput;
        }

        private void InstallBotActions(BotMoveInput moveInput, BotFightInput fightInput, BotInputStateMachine machine)
        {
            ActionStash stash = new(moveInput, fightInput, _botParameters);
            
            _botActionFactory.InstallActions(stash, moveInput, machine);
        }

        private IMoveInput InstallPlayerInput(ContainerBuilder builder)
        {
            UserInput input = new();
            PlayerMoveInputReader moveInputReader = new(input);
            PlayerAttackInputReader attackInputReader = new(input);
            Dictionary<int, DistanceValidator> directions = _playerDirectionValidationFactory.Produce();
            ValidatedInput validatedInput = new(_playerParameters.transform, moveInputReader, directions);
            
            input.Enable(); // управление инпутом в другое место
            builder.AddSingleton(moveInputReader);
            builder.AddSingleton(attackInputReader);
            builder.AddSingleton(input);
            return validatedInput;
        }

        private PositionTranslation InstallPlayerMovement(IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(_playerParameters.transform, _playerParameters.MoveSpeed);
            _playerMovement.Initialize(moveInput, positionTranslation);
            return positionTranslation;
        }
        
        private PositionTranslation InstallBotMovement(IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(_botParameters.transform, _botParameters.MoveSpeed);
            _botMovement.Initialize(moveInput, positionTranslation);
            return positionTranslation;
        }
    }
}