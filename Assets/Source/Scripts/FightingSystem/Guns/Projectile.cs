using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Spherecaster _spherecaster;

        private Subject<Projectile> _executed;
        private CancellationTokenSource _cancellationTokenSource;
        private Damage _damage;
        
        public Observable<Projectile> Executed => _executed;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PullableObjectReleaseTrigger>(out _))
                _executed.OnNext(this);
        }
        
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
            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                if (_spherecaster.TryFindDamageable(out IDamageable<Damage> damageable))
                {
                    damageable.AcceptDamage(_damage);
                    _executed.OnNext(this);
                    return;
                }

                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }
        }
    }
}