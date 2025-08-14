using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public abstract class Attacker : MonoBehaviour, IContinuous
    {
        private Dictionary<IAttack, Spherecaster> _attacks;
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsContinuing { get; private set; }

        public void SetAttacks(Dictionary<IAttack, Spherecaster> attacks)
        {
            if (attacks == null)
                throw new ArgumentNullException(nameof(attacks));

            _attacks = attacks;
        }

        protected void SubscribeStateMachine(IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            if (stateMachine == null)
                throw new ArgumentNullException(nameof(stateMachine));

            stateMachine.Value
                .Where(state => state is AttackState)
                .Subscribe(state => Attack(state.Type))
                .AddTo(this);

            stateMachine.Value
                .Where(state => state is HittedState)
                .Subscribe(_ => CancelAttack())
                .AddTo(this);

            conditionAddable.Add<AttackState>(_ => IsContinuing);
        }

        private void Attack(Type state)
        {
            if (IsContinuing)
                return;

            IAttack attackKey = _attacks.Keys.FirstOrDefault(attack => attack.RequiredState == state);

            if (attackKey == null)
                throw new KeyNotFoundException(nameof(attackKey));

            IsContinuing = true;
            _cancellationTokenSource = new CancellationTokenSource();
            AttackDelayed(attackKey, _attacks[attackKey]).Forget();
        }

        private async UniTaskVoid AttackDelayed(IAttack attack, Spherecaster spherecaster)
        {
            await UniTask.WaitForSeconds(attack.Delay, cancellationToken: _cancellationTokenSource.Token,
                cancelImmediately: true);
            bool isHitted = spherecaster.TryFindDamageable(out IDamageable damageable);

            if (isHitted)
                attack.ApplyDamage(damageable);

            await UniTask.WaitForSeconds(attack.Duration - attack.Delay);
            CancelAttack();
        }

        private void CancelAttack()
        {
            _cancellationTokenSource?.Cancel();
            IsContinuing = false;
        }
    }
}