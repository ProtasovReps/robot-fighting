using FightingSystem;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem.Bot;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotTransitionFactory : StateTransitionFactory
    {
        [SerializeField] private HitReader _hitReader;
        
        private BotMoveInput _botMoveInput;
        private BotAttackInput _botAttackInput;
        
        [Inject]
        private void Inject(BotMoveInput botMoveInput, BotAttackInput botAttackInput)
        {
            _botMoveInput = botMoveInput;
            _botAttackInput = botAttackInput;
        }
        
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            StateMachine stateMachine)
        {
            builder.Reset<AttackState>(false);
            builder.BuildGlobal<UpHittedState>(false, typeof(DownHittedState));
            builder.BuildGlobal<DownHittedState>(false, typeof(UpHittedState));
            builder.BuildGlobal<AttackState>(false, typeof(UpHittedState), typeof(DownHittedState));
            
            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, int>(_botMoveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, int>(_botMoveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, int>(_botMoveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<UpHittedState, Unit>(_hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(_hitReader.LegsHitted, builder.Get<DownHittedState>())
                .InitializeTransition<PunchState, Unit>(_botAttackInput.UpAttack, builder.Get<AttackState>())
                .InitializeTransition<KickState, Unit>(_botAttackInput.DownAttack, builder.Get<AttackState>());
        }
    }
}