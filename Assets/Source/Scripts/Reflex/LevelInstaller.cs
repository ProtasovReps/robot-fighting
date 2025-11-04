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
using YG;
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
            PlayerHealth health = new(YG2.saves.HealthStat);
            ImplantPlaceHolderStash placeHolderStash = _playerImplantFactory.Produce();
            HitReader hitReader = _playerHitFactory.Produce(health, playerStateMachine, conditionBuilder);
            PlayerDeath death = new(hitReader, health, conditionBuilder);
            IMoveInput moveInput = InstallPlayerInput(builder, death);
            AttackAnimationOverrider animationOverrider = new(_playerAnimator);

            new Stretch(playerStateMachine, conditionBuilder);
            
            _playerAttackFactory.Produce(placeHolderStash);
            
            PositionTranslation positionTranslation = InstallPlayerMovement(moveInput);
            
            animationFactory.Produce(_playerAnimatedCharacter,
                _playerAnimator, playerStateMachine, _playerParameters, positionTranslation, YG2.saves.SpeedStat);
            
            animationOverrider.Override(placeHolderStash);
           
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
            ImplantPlaceHolderStash placeHolderStash = _botImplantFactory.Produce();
            HitReader hitReader = _botHitFactory.Produce(health, botStateMachine, conditionBuilder);
            BotDeath death = new BotDeath(hitReader, health, conditionBuilder);
            IMoveInput moveInput = InstallBotInput(builder, death);
            AttackAnimationOverrider animationOverrider = new(_botAnimator);

            _botAttackFactory.Produce(placeHolderStash);
            
            PositionTranslation positionTranslation = InstallBotMovement(moveInput);
            
            animationFactory.Produce(_botAnimatedCharacter, 
                _botAnimator, botStateMachine, _botParameters, positionTranslation, _botParameters.MoveSpeed);
            animationOverrider.Override(placeHolderStash);

            builder.AddSingleton(health);
            builder.AddSingleton(conditionBuilder);
            builder.AddSingleton(botStateMachine);
        }

        private IMoveInput InstallBotInput(ContainerBuilder builder, BotDeath death)
        {
            State[] states = new BotInputStateFactory().Produce();
            BotInputStateMachine stateMachine = new(states);
            BotInputConditionBuilder conditionBuilder = new();
            
            BotMoveInput botMoveInput = new();
            BotFightInput botFightInput = new();
            Dictionary<int, DistanceValidator> directions = _botDirectionValidationFactory.Produce();
            ValidatedInput validatedBotInput = new(_botParameters.transform, botMoveInput, directions);
            
            InstallBotActions(botMoveInput, botFightInput, stateMachine);

            _botTransitionFactory.Initialize(validatedBotInput, botFightInput, death);

            builder.AddSingleton(stateMachine);
            builder.AddSingleton(conditionBuilder);
            return validatedBotInput;
        }

        private void InstallBotActions(BotMoveInput moveInput, BotFightInput fightInput, BotInputStateMachine machine)
        {
            ActionStash stash = new(moveInput, fightInput, _botParameters);
            
            _botActionFactory.InstallActions(stash, moveInput, machine);
        }

        private IMoveInput InstallPlayerInput(ContainerBuilder builder, PlayerDeath death)
        {
            UserInput input = new();
            PlayerMoveInputReader moveInputReader = new(input);
            PlayerAttackInputReader attackInputReader = new(input);
            Dictionary<int, DistanceValidator> directions = _playerDirectionValidationFactory.Produce();
            ValidatedInput validatedInput = new(_playerParameters.transform, moveInputReader, directions);
            
            _playerTransitionFactory.Initialize(moveInputReader, attackInputReader, death);
            
            input.Enable(); // управление инпутом в другое место
            builder.AddSingleton(input);
            return validatedInput;
        }

        private PositionTranslation InstallPlayerMovement(IMoveInput moveInput)
        {
            PositionTranslation positionTranslation = new(_playerParameters.transform, YG2.saves.SpeedStat);
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