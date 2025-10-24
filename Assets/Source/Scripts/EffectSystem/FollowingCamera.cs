using UnityEngine;

namespace EffectSystem
{
    [RequireComponent(typeof(Camera))]
    public class FollowingCamera : MonoBehaviour 
    {
        [SerializeField] private Transform _firstFighter;
        [SerializeField] private Transform _secondFighter;
        [SerializeField] private float _maxCameraDistance;
        [SerializeField] private float _minCameraDistance;
        [SerializeField] private float _followSpeed;
        [SerializeField] private float _zoomSpeed;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            Animate();
        }

        private void Animate()
        {
            Centralize();
            Zoom();
        }

        private void Centralize()
        {
            float centralRightPosition = (_firstFighter.position.x + _secondFighter.position.x) * 0.5f;
            var newPosition = new Vector3(centralRightPosition, _transform.position.y, _transform.position.z);

            _transform.position = Vector3.Lerp(_transform.position, newPosition, _followSpeed * Time.deltaTime);
        }

        private void Zoom()
        {
            float playerDistance = Mathf.Abs(_firstFighter.position.x - _secondFighter.position.x);
            float targetForwardPosition =
                Mathf.Lerp(_minCameraDistance, _maxCameraDistance, playerDistance / _maxCameraDistance);
            float newForwardPosition =
                Mathf.Lerp(_transform.position.z, -targetForwardPosition, _zoomSpeed * Time.deltaTime);

            _transform.position = new Vector3(_transform.position.x, _transform.position.y, newForwardPosition);
        }
    }
}