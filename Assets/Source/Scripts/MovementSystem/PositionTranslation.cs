using Extensions;
using InputSystem;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class PositionTranslation : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private DistanceValidator _distanceValidator;
       
        private InputReader _inputReader;
        private Transform _transform;
        
        [Inject]
        private void Inject(InputReader inputReader)
        {
            _inputReader = inputReader;
        }
        
        private void Start()
        {
            _transform = transform;
            _inputReader.DirectionChanged
                .Subscribe(TranslatePosition)
                .AddTo(this);
        }

        private void TranslatePosition(float direction)
        {
            float targetDistance = direction * _moveSpeed * Time.deltaTime;
            Vector3 targetPosition = _transform.position + Vector3.right * targetDistance;

            if (_distanceValidator.IsValidDistance(targetPosition) == false)
                return;

            _transform.position = targetPosition;
        }
    }
}
