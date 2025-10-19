using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions.Exceptions;
using FightingSystem.Attacks;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public abstract class Attacker : MonoBehaviour, IContinuous
    {
        private Dictionary<Type, Attack> _attacks;
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsContinuing { get; private set; }

        public void SetAttacks(Dictionary<Type, Attack> attacks)
        {
            if (attacks == null)
                throw new ArgumentNullException(nameof(attacks));

            _attacks = attacks;
        }

        protected void Subscribe(IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            if (stateMachine == null)
                throw new ArgumentNullException(nameof(stateMachine));

            stateMachine.Value
                .Where(state => state is AttackState)
                .Subscribe(state => PrepareAttack(state.Type))
                .AddTo(this);

            stateMachine.Value
                .Where(state => state is HittedState)
                .Subscribe(_ => CancelAttack())
                .AddTo(this);

            conditionAddable.Add<AttackState>(_ => IsContinuing);
        }

        private void PrepareAttack(Type state)
        {
            if (IsContinuing)
                return;

            if (_attacks.ContainsKey(state) == false)
                throw new StateNotFoundException(nameof(state));

            IsContinuing = true;
            _cancellationTokenSource = new CancellationTokenSource();

            AttackDelayed(_attacks[state]).Forget();
        }

        private async UniTaskVoid AttackDelayed(Attack attack)
        {
            await attack.Launch(_cancellationTokenSource);
            CancelAttack();
        }

        private void CancelAttack()
        {
            _cancellationTokenSource?.Cancel();
            IsContinuing = false;
        }
    }
}