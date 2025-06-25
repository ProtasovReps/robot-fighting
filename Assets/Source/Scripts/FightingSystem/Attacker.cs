using Cysharp.Threading.Tasks;
using Extensions;
using Interface;
using UnityEngine;

namespace FightingSystem
{
    public abstract class Attacker : MonoBehaviour, IExecutable
    {
        [SerializeField] private Spherecaster _spherecaster;

        private IAttack _attack;

        public bool IsExecuting { get; private set; }

        public void Initialize(IAttack attack)
        {
            _attack = attack;
        }

        protected void Attack()
        {
            if (IsExecuting)
                return;

            IsExecuting = true;
            AttackDelayed().Forget();
        }

        private async UniTaskVoid AttackDelayed()
        {
            await UniTask.WaitForSeconds(_attack.Delay);
            bool isHitted = _spherecaster.TryFindDamageable(out IDamageable damageable);

            if (isHitted)
            {
                _attack.ApplyDamage(damageable);
            }

            IsExecuting = false;
        }
    }
}