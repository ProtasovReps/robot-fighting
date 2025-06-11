using NewInputSystem;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

namespace Movement
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _fallSpeed;
        
        private float _startHeight;
        private InputReader _inputReader;
        private Transform _transform;

        [Inject]
        private void Inject(InputReader inputReader)
        {
            _inputReader = inputReader;
        }
        
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
            
            TranslateUp().Forget();
        }

        private async UniTaskVoid TranslateUp()
        {
            Vector3 targetPosition = _transform.position + _transform.up * _jumpHeight;
            float expiredTime = 0f;

            while (expiredTime < _jumpTime)
            {
                float upPosition = Mathf.Lerp(_startHeight, targetPosition.y, expiredTime / _jumpTime);

                _transform.position = new Vector3(_transform.position.x, upPosition, _transform.position.z);
                expiredTime += Time.deltaTime;
                await UniTask.Yield();
            }
            
            TranslateDown().Forget();
        }

        private async UniTaskVoid TranslateDown()
        {
            while (_transform.position.y > _startHeight)
            {
                _transform.position += _fallSpeed * Time.deltaTime * Physics.gravity;
                await UniTask.Yield();
            }
        }
    }
}