using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace FightingSystem.Guns
{
    public abstract class ProjectileShooter : Shooter
    {
        private readonly Subject<Projectile> _executed = new();
        
        [SerializeField] private float _shootForce;
        
        private ProjectilePool _projectilePool;
        private CancellationTokenSource _cancellationTokenSource;
        private Vector3 _shootDirection;

        protected Observable<Projectile> Executed => _executed;
        
        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        public virtual void Initialize(ProjectilePool projectilePool)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _shootDirection = -(Vector3.right * transform.position.x).normalized;

            _projectilePool = projectilePool;
        }
       
        public override void Shoot()
        {
            Projectile projectile = _projectilePool.Get();
            
            Follow(projectile).Forget();
        }

        protected abstract void TranslateProjectile(Projectile projectile, Vector3 direction, float force);
        
        private async UniTaskVoid Follow(Projectile projectile)
        {
            while (projectile.gameObject.activeSelf && _cancellationTokenSource.IsCancellationRequested == false)
            {
               TranslateProjectile(projectile, _shootDirection, _shootForce);
               await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }

            _executed.OnNext(projectile);
        }
    }
}