using Extensions;
using FightingSystem.Dying;
using HitSystem;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem;
using InputSystem.Bot;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotTransitionFactory : StateTransitionFactory
    {
        [SerializeField] private HitReader _hitReader;
        
        private ValidatedInput _botMoveInput;
        private BotFightInput _botFightInput;
        private BotDeath _death;
        
        [Inject]
        private void Inject(ValidatedInput botMoveInput, BotFightInput botFightInput, BotDeath death)
        {
            _botMoveInput = botMoveInput;
            _botFightInput = botFightInput;
            _death = death;
        }
        
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            TransitionInitializer initializer)
        {
            Observable<Unit> armAttack = _botFightInput.GetObservable(MotionHashes.ArmAttack);
            Observable<Unit> legAttack = _botFightInput.GetObservable(MotionHashes.LegAttack);
            Observable<Unit> special = _botFightInput.GetObservable(MotionHashes.Special);
            Observable<Unit> super = _botFightInput.GetObservable(MotionHashes.Super);
            Observable<Unit> block = _botFightInput.GetObservable(MotionHashes.Block);
            
            builder.Reset<BlockState>(false);
            builder.Reset<AttackState>(false);
            
            builder.MergeGlobal<DeathState>(false);
            builder.MergeGlobal<UpHittedState>(false, typeof(DownHittedState), typeof(DeathState));
            builder.MergeGlobal<DownHittedState>(false, typeof(UpHittedState), typeof(DeathState));
            builder.MergeGlobal<AttackState>(false, typeof(UpHittedState), typeof(DownHittedState));
            builder.MergeGlobal<BlockState>(false, typeof(DownHittedState), typeof(DeathState));

            initializer
                .InitializeTransition<IdleState, int>(_botMoveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, int>(_botMoveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, int>(_botMoveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<UpHittedState, Unit>(_hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(_hitReader.LegsHitted, builder.Get<DownHittedState>())
                .InitializeTransition<UpAttackState, Unit>(armAttack, builder.Get<AttackState>())
                .InitializeTransition<DownAttackState, Unit>(legAttack, builder.Get<AttackState>())
                .InitializeTransition<SpecialAttackState, Unit>(special, builder.Get<AttackState>())
                .InitializeTransition<BlockState, Unit>(block, builder.Get<BlockState>())
                .InitializeTransition<UpDeathState, Unit>(_death.UpDeath, builder.Get<DeathState>())
                .InitializeTransition<DownDeathState, Unit>(_death.DownDeath, builder.Get<DeathState>());
        }
    }
}