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
        
        [Inject]
        private void Inject(ValidatedInput botMoveInput, BotFightInput botFightInput)
        {
            _botMoveInput = botMoveInput;
            _botFightInput = botFightInput;
        }
        
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            StateMachine stateMachine)
        {
            builder.Reset<BlockState>(false);
            builder.Reset<AttackState>(false);
            
            builder.BuildGlobal<UpHittedState>(false, typeof(DownHittedState));
            builder.BuildGlobal<DownHittedState>(false, typeof(UpHittedState));
            builder.BuildGlobal<AttackState>(false, typeof(UpHittedState), typeof(DownHittedState));
            builder.BuildGlobal<BlockState>(false, typeof(DownHittedState));
        
            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, int>(_botMoveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, int>(_botMoveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, int>(_botMoveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<UpHittedState, Unit>(_hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(_hitReader.LegsHitted, builder.Get<DownHittedState>())
                .InitializeTransition<UpAttackState, Unit>(_botFightInput.UpAttack, builder.Get<AttackState>())
                .InitializeTransition<DownAttackState, Unit>(_botFightInput.DownAttack, builder.Get<AttackState>())
                .InitializeTransition<SpecialAttackState, Unit>(_botFightInput.SpecialAttack, builder.Get<AttackState>())
                .InitializeTransition<BlockState, Unit>(_botFightInput.Block, builder.Get<BlockState>());
        }
    }
}