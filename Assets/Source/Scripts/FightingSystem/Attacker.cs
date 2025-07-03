using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extensions;
using Interface;
using UnityEngine;

namespace FightingSystem
{
    public abstract class Attacker : MonoBehaviour, IExecutable
    {
        private Dictionary<IAttack, Spherecaster> _attacks;

        public bool IsExecuting { get; private set; }

        public void Initialize(Dictionary<IAttack, Spherecaster> attacks)
        {
            if (attacks == null)
            {
                throw new ArgumentNullException(nameof(attacks));
            }

            _attacks = attacks;
        }

        protected void Attack(Type state)
        {
            if (IsExecuting)
            {
                return;
            }

            IAttack attackKey = null;
                
            foreach (IAttack attack in _attacks.Keys)
            {
                if (attack.RequiredState == state)
                {
                    attackKey = attack;
                    break;
                }
            }

            if (attackKey == null)
            {
                throw new KeyNotFoundException(nameof(attackKey));
            }
            
            IsExecuting = true;
            AttackDelayed(attackKey, _attacks[attackKey]).Forget();
        }

        private async UniTaskVoid AttackDelayed(IAttack attack, Spherecaster spherecaster)
        {
            Debug.Log("Стреляю");
            await UniTask.WaitForSeconds(attack.Delay);
            bool isHitted = spherecaster.TryFindDamageable(out IDamageable damageable);

            if (isHitted)
            {
                attack.ApplyDamage(damageable);
            }

            IsExecuting = false;
            Debug.Log("Выстрелил");
        }
    }
}