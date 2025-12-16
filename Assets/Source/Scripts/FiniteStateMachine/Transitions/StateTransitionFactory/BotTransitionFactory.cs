using CharacterSystem.Dying;
using Extensions;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using HitSystem;
using InputSystem;
using InputSystem.Bot;
using R3;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotTransitionFactory : StateTransitionFactory<BotStateMachine, BotConditionBuilder>
    {
        [SerializeField] private HitReader _hitReader;

        private ValidatedInput _botMoveInput;
        private BotFightInput _botFightInput;
        private BotDeath _death;

        public void Initialize(ValidatedInput botMoveInput, BotFightInput botFightInput, BotDeath death)
        {
            _botMoveInput = botMoveInput;
            _botFightInput = botFightInput;
            _death = death;
        }

        protected override void InitializeConditions(ConditionBuilder builder)
        {
            builder.Reset<BlockState>(false);
            builder.Reset<AttackState>(false);

            builder.MergeGlobal<DeathState>(false);

            builder.MergeGlobal<UpHittedState>(
                false, typeof(DownHittedState), typeof(DeathState));
            builder.MergeGlobal<DownHittedState>(
                false, typeof(UpHittedState), typeof(DeathState));
            
            builder.MergeGlobal<AttackState>(
                false, 
                typeof(UpHittedState),
                typeof(DownHittedState),
                typeof(MoveRightState));
            
            builder.MergeGlobal<BlockState>(
                false, 
                typeof(DownHittedState),
                typeof(DeathState),
                typeof(MoveRightState));
        }

        protected override void InstallTransitions(StateMachine machine, ConditionBuilder builder, Disposer disposer)
        {
            Observable<Unit> armAttack = _botFightInput.GetObservable(MotionHashes.ArmAttack);
            Observable<Unit> legAttack = _botFightInput.GetObservable(MotionHashes.LegAttack);
            Observable<Unit> special = _botFightInput.GetObservable(MotionHashes.Special);
            Observable<Unit> super = _botFightInput.GetObservable(MotionHashes.Super);
            Observable<Unit> block = _botFightInput.GetObservable(MotionHashes.Block);

            var soloInitializer = new TransitionInitializer(new SoloTransitionFactory(), machine)
                .InitializeTransition<IdleState, float>(
                    _botMoveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, float>(
                    _botMoveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, float>(
                    _botMoveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<UpDeathState, Unit>(
                    _death.UpDeath, builder.Get<DeathState>())
                .InitializeTransition<DownDeathState, Unit>(
                    _death.DownDeath, builder.Get<DeathState>());

            var repeatableInitializer = new TransitionInitializer(new RepeatableTransitionFactory(), machine)
                .InitializeTransition<UpAttackState, Unit>(
                    armAttack, builder.Get<AttackState>())
                .InitializeTransition<DownAttackState, Unit>(
                    legAttack, builder.Get<AttackState>())
                .InitializeTransition<SpecialAttackState, Unit>(
                    special, builder.Get<AttackState>())
                .InitializeTransition<SuperAttackState, Unit>(
                    super, builder.Get<AttackState>())
                .InitializeTransition<BlockState, Unit>(
                    block, builder.Get<BlockState>())
                .InitializeTransition<UpHittedState, Unit>(
                    _hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(
                    _hitReader.LegsHitted, builder.Get<DownHittedState>());

            disposer.Add(soloInitializer);
            disposer.Add(repeatableInitializer);
        }
    }
}