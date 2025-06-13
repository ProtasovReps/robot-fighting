using UnityEngine;

namespace Extensions
{
    public class DistanceValidator : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _validDistance;

        public bool IsValidDistance(Vector3 position)
        {
            float offset = _target.transform.position.x - position.x;
            return Mathf.Abs(offset) >_validDistance;
        }
    }
}
