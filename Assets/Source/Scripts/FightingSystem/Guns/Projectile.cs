using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using FightingSystem.AttackDamage;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Spherecaster _spherecaster;
        [SerializeField] private float _lifeTime;
        
        private Subject<Projectile> _executed;
        private CancellationTokenSource _cancellationTokenSource;
        private Damage _damage;
        
        public Observable<Projectile> Executed => _executed;

        public void Initialize(LayerMask opponentLayer, Damage damage)
        {
            _executed = new Subject<Projectile>();
            _damage = damage;

            _spherecaster.Initialize(opponentLayer);
        }

        public void Activate()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            CheckCollision().Forget();
        }

        public void Deactivate()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid CheckCollision()
        {
            float elapsedTime = 0f;
            
            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                if (_spherecaster.TryFindDamageable(out IDamageable<Damage> damageable))
                {
                    damageable.AcceptDamage(_damage);
                    InvokeExecuted();
                    return;
                }

                if (elapsedTime >= _lifeTime)
                {
                    InvokeExecuted();
                }

                elapsedTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }
        }

        private void InvokeExecuted()
        {
            _executed.OnNext(this);
        }
    }
}