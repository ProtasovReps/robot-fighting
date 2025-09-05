using FightingSystem;
using Interface;
using UnityEngine;

namespace Extensions
{
    public class Spherecaster : MonoBehaviour
    {
        private const int MaxHits = 1;

        [SerializeField] [Min(0.1f)] private float _radius;

        private LayerMask _searchedLayer;
        private Transform _transform;
        private Collider[] _hits;

        public void Initialize(LayerMask searchedLayer)
        {
            _transform = transform;
            _searchedLayer = searchedLayer;
            _hits = new Collider[MaxHits];
        }

        public bool TryFindDamageable(out IDamageable<Damage> damageable)
        {
            int hits = Physics.OverlapSphereNonAlloc(_transform.position, _radius, _hits, _searchedLayer);
            
            damageable = null;

            if (hits == 0)
                return false;

            if (_hits[0].transform.TryGetComponent(out damageable) == false)
                return false;

            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}