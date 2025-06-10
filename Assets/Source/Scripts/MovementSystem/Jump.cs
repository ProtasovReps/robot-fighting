using NewInputSystem;
using System.Collections;
using UnityEngine;

namespace Movement
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private InputReader _inputReader; // Reflex

        private float _startHeight;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _inputReader.JumpPressed += OnJumpPressed;
            _startHeight = transform.position.y;
        }

        private void OnDestroy()
        {
            _inputReader.JumpPressed -= OnJumpPressed;
        }

        private void OnJumpPressed()
        {
            if (_transform.position.y > _startHeight)
                return;

            StartCoroutine(TranslateUp());
        }

        private IEnumerator TranslateUp() // лучше на эвейтах (юнитаск)
        {
            Vector3 targetPosition = _transform.position + _transform.up * _jumpHeight;
            float expiredTime = 0f;

            while (expiredTime < _jumpTime)
            {
                float upPosition = Mathf.Lerp(_startHeight, targetPosition.y, expiredTime / _jumpTime);

                _transform.position = new Vector3(_transform.position.x, upPosition, _transform.position.z);
                expiredTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(TranslateDown());
        }

        private IEnumerator TranslateDown()
        {
            while (_transform.position.y > _startHeight)
            {
                _transform.position += _fallSpeed * Time.deltaTime * Physics.gravity;
                yield return null;
            }
        }
    }
}