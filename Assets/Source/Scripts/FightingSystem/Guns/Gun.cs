using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class Gun : Shooter
    {
        [SerializeField] private float _shootForce;
        
        private ProjectilePool _projectilePool;
        private CancellationTokenSource _cancellationTokenSource;
        private Vector3 _shootDirection;
        
        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Initialize(ProjectilePool projectilePool)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _shootDirection = -(Vector3.right * transform.position.x).normalized;

            _projectilePool = projectilePool;
        }
       
        public override void Shoot()
        {
            Projectile projectile = _projectilePool.Get();
            
            FollowBullet(projectile).Forget();
        }

        private async UniTaskVoid FollowBullet(Projectile projectile)
        {
            while (projectile.gameObject.activeSelf && _cancellationTokenSource.IsCancellationRequested == false)
            {
                projectile.transform.position += _shootDirection * _shootForce * Time.deltaTime;
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }
        }
    }
}