using Extensions;
using NewInputSystem;
using UnityEngine;

namespace Movement
{
    public class PositionTranslation : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private InputReader _inputReader; // сервис, получать через Reflex
        [SerializeField] private DistanceValidator _distanceValidator;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            TranslatePosition();
        }

        private void TranslatePosition()
        {
            float targetDistance = _inputReader.Direction * _moveSpeed * Time.deltaTime;
            Vector3 targetPosition = _transform.position + _transform.right * targetDistance;

            if (_distanceValidator.IsValidDistance(targetPosition) == false)
                return;

            _transform.position = targetPosition;
        }
    }
}
