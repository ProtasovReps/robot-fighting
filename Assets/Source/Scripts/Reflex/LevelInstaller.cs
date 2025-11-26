using System;
using System.Collections.Generic;
using AnimationSystem;
using AnimationSystem.Factory;
using CharacterSystem;
using CharacterSystem.Parameters;
using HitSystem;
using Extensions;
using FightingSystem;
using CharacterSystem.Dying;
using FightingSystem.Factory;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.Factory;
using FiniteStateMachine.Transitions.Factory;
using CharacterSystem.CharacterHealth;
using EffectSystem.Particle;
using ImplantSystem;
using ImplantSystem.Factory;
using InputSystem;
using InputSystem.Bot;
using InputSystem.Bot.Factory;
using Interface;
using MovementSystem;
using Reflex.Core;
using UI.Store;
using UnityEngine;
using YG;
using YG.Saver;
using BotMovement = MovementSystem.BotMovement;
using State = FiniteStateMachine.States.State;

namespace Reflex
{
    public class LevelInstaller : MonoBehaviour, IInstaller
    {
        [Header("Player")]
        [SerializeField] private PlayerTransitionFactory _playerTransitionFactory;
        [SerializeField] private PlayerAttackFactory _playerAttackFactory;
        [SerializeField] private PlayerHitFactory _playerHitFactory;
        [SerializeField] private DirectionValidationFactory _playerDirectionValidationFactory;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerParameters _playerParameters;
        [SerializeField] private PlayerImplantFactory _playerImplantFactory;
        [SerializeField] private Disposer _disposer;
        [SerializeField] private ProgressSaver _saver;
        [SerializeField] private DefaultSavesInstaller _defaultSavesInstaller;
        [SerializeField] private FighterSpawner _fighterSpawner;
        [SerializeField] private PlayerHitParticles _playerHitParticles;
        [SerializeField] private AnimationStateMapper _animationStateMapper;
        
        [Header("Bot")]
        [SerializeField] private BotTransitionFactory _botTransitionFactory;
        [SerializeField] private BotAttackFactory _botAttackFactory;
        [SerializeField] private BotHitFactory _botHitFactory;
        [SerializeField] private BotInputTransitionFactory _botInputTransitionFactory;
        [SerializeField] private BotMovement _botMovement;
        [SerializeField] private DirectionValidationFactory _botDirectionValidationFactory;
        [SerializeField] private BotParameters _botParameters;
        [SerializeField] private BotImplantFactory _botImplantFactory;
        [SerializeField] private ActionFactory _botActionFactory;
        [SerializeField] private AnimatedCharacter _botAnimatedCharacter;

        private Fighter _player;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            if(YG2.saves.UpAttackImplant == null)
                _defaultSavesInstaller.Install();

            _player = _fighterSpawner.Spawn(); 
            _player.Initialize();    
            
            AnimationFactory animationFactory = new();
            
            InstallBot(animationFactory, containerBuilder);
            InstallPlayer(animationFactory, containerBuilder);
            InstallSaves(containerBuilder);
        }

        private void InstallPlayer(AnimationFactory animationFactory, ContainerBuilder builder)
        {
            State[] states = new PlayerStateFactory().Produce();
            PlayerStateMachine playerStateMachine = new(states);
            PlayerConditionBuilder conditionBuilder = new();
            PlayerHealth health = new(YG2.saves.HealthStat);
            EquipedImplantSaver equipedImplantSaver = new(new Hasher<ImplantView>());
            
            _playerHitFactory.Initialize(_player.HitColliderStash);
            _playerHitParticles.Initialize(_player.HitEffectStash);
            _playerImplantFactory.Initialize(_player.ImplantPlaceHolderStash, equipedImplantSaver);
            
            ImplantPlaceHolderStash placeHolderStash = _playerImplantFactory.Produce();
            HitReader hitReader = _playerHitFactory.Produce(health, playerStateMachine, conditionBuilder, _disposer);
            PlayerDeath death = new(hitReader, health, conditionBuilder);
            IMoveInput moveInput = InstallPlayerInput(builder, death);

            _animationStateMapper.Initialize();
            
            AttackAnimationOverrider overrider = new(_player.AnimatedCharacter.Animator, _animationStateMapper);
            SuperAttackCharge attackCharge = new(hitReader, playerStateMachine, conditionBuilder);
            
            _disposer.Add(death);
            _disposer.Add(attackCharge);
            _disposer.Add(new Stretch(playerStateMachine, conditionBuilder));
            
            _playerAttackFactory.Produce(placeHolderStash);
            
            PositionTranslation positionTranslation = InstallPlayerMovement(moveInput);
            
            animationFactory.Produce(_player.AnimatedCharacter, 
                playerStateMachine, _playerParameters, positionTranslation, _disposer, YG2.saves.SpeedStat);
            
            overrider.Override(placeHolderStash);
           
            builder.AddSingleton(attackCharge);
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
            HitReader hitReader = _botHitFactory.Produce(health, botStateMachine, conditionBuilder, _disposer);
            BotDeath death = new BotDeath(hitReader, health, conditionBuilder);
            IMoveInput moveInput = InstallBotInput(builder, death);

            _disposer.Add(death);
            _botAttackFactory.Produce(placeHolderStash);
            
            PositionTranslation positionTranslation = InstallBotMovement(moveInput);
            
            animationFactory.Produce(_botAnimatedCharacter,
                botStateMachine, _botParameters, positionTranslation, _disposer, _botParameters.MoveSpeed);

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
            
            _disposer.Add(botMoveInput);
            _disposer.Add(validatedBotInput);
            
            InstallBotActions(botMoveInput, botFightInput, stateMachine);

            _botTransitionFactory.Initialize(validatedBotInput, botFightInput, death);

            builder.AddSingleton(stateMachine);
            builder.AddSingleton(conditionBuilder);
            return validatedBotInput;
        }

        private void InstallBotActions(BotMoveInput moveInput, BotFightInput fightInput, BotInputStateMachine machine)
        {
            ActionStash stash = new(moveInput, fightInput, _botParameters);
            
            _botActionFactory.InstallActions(stash, moveInput, machine, _disposer);
        }

        private IMoveInput InstallPlayerInput(ContainerBuilder builder, PlayerDeath death)
        {
            UserInput input = new();
            PlayerMoveInputReader moveInputReader = new(input);
            PlayerAttackInputReader attackInputReader = new(input);
            Dictionary<int, DistanceValidator> directions = _playerDirectionValidationFactory.Produce();
            ValidatedInput validatedInput = new(_playerParameters.transform, moveInputReader, directions);
            
            _playerTransitionFactory.Initialize(moveInputReader, attackInputReader, death);

            _disposer.Add(validatedInput);
            _disposer.Add(moveInputReader);
            _disposer.Add(input);
            
            input.Enable();
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

        private void InstallSaves(ContainerBuilder builder)
        {
            Dictionary<StatType, float> startStats = new Dictionary<StatType, float>
            {
                { StatType.Health, YG2.saves.HealthStat },
                { StatType.Damage, YG2.saves.DamageStat },
                { StatType.Speed, YG2.saves.SpeedStat },
                { StatType.Block, YG2.saves.BlockStat }
            };
            
            Wallet wallet = new(YG2.saves.Money);
            CharacterStats stats = new(startStats);
            
            WalletSaver walletSaver = new(wallet);
            CharacterStatSaver statSaver = new(stats);
            
            _saver.Add(walletSaver);
            _saver.Add(statSaver);
            
            builder.AddSingleton(_saver);
            builder.AddSingleton(wallet, typeof(IMoneyAddable), typeof(IValueChangeable<int>));
            builder.AddSingleton(stats);
        }
    }
}