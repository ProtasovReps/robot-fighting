using FighterSystem;
using Interface;
using UnityEngine;

namespace Extensions
{
    public class Raycaster : MonoBehaviour
    {
        private const int MaxHits = 1;
        
        [SerializeField] private LayerMask _searchedLayer;
        [SerializeField] private float _radius;

        private Transform _transform;
        private Collider[] _hits;
        
        private void Awake()
        {
            _transform = transform;
            _hits = new Collider[MaxHits];
        }

        private void Update()
        {
            Debug.Log(TryFindDamageable(out IDamageable damageable));
        }

        public bool TryFindDamageable(out IDamageable damageable)
        {
            int hits = Physics.OverlapSphereNonAlloc(_transform.position, _radius, _hits, _searchedLayer);
            damageable = null;

            if (hits == 0)
                return false;

            if (_hits[0].transform.TryGetComponent(out Fighter fighter) == false)
                return false;

            damageable = fighter.Health;
            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}