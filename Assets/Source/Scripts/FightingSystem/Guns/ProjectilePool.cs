using System.Collections.Generic;
using R3;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private Queue<Projectile> _freeProjectiles;
        private List<Projectile> _projectiles;
        private ProjectileFactory _projectileFactory;
        
        private void OnDestroy()
        {
            foreach (Projectile projectile in _projectiles)
            {
                projectile.Deactivate();
            }
        }

        public void Initialize(ProjectileFactory projectileFactory)
        {
            _freeProjectiles = new Queue<Projectile>();
            _projectiles = new List<Projectile>();

            _projectileFactory = projectileFactory;
        }

        public Projectile Get()
        {
            Projectile projectile;

            if (_freeProjectiles.Count > 0)
            {
                projectile = _freeProjectiles.Dequeue();
            }
            else
            {
                projectile = Create();
            }

            ActivateProjectile(projectile);
            return projectile;
        }

        private Projectile Create()
        {
            Projectile newProjectile = _projectileFactory.Produce();
            
            newProjectile.Executed
                .Subscribe(Release)
                .AddTo(this);

            _projectiles.Add(newProjectile);
            return newProjectile;
        }

        private void ActivateProjectile(Projectile projectile)
        {
            projectile.transform.position = _spawnPoint.position;
            projectile.gameObject.SetActive(true);
            projectile.Activate();
        }

        private void Release(Projectile projectile)
        {
            projectile.Deactivate();
            projectile.gameObject.SetActive(false);
            _freeProjectiles.Enqueue(projectile);
        }
    }
}