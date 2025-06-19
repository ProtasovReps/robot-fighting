using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using InputSystem;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _fallSpeed;

        private float _startHeight;
        private InputReader _inputReader;
        private Transform _transform;
        private IStateChangeable _stateMachine;

        public bool IsGrounded { get; private set; } = true;

        [Inject]
        private void Inject(InputReader inputReader, IStateChangeable stateMachine)
        {
            _inputReader = inputReader;
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _transform = transform;
            _startHeight = _transform.position.y;
            _inputReader.JumpPressed
                .Where(_ => _stateMachine.CurrentState.CurrentValue.Type == typeof(JumpState) ||
                            _stateMachine.CurrentState.CurrentValue.Type == typeof(MoveState))
                .Subscribe(_ => OnJumpPressed())
                .AddTo(this);
        }

        private void OnJumpPressed()
        {
            if (IsGrounded == false)
                return;

            IsGrounded = false;
            TranslateUp().Forget();
        }

        // может в будущем понадобится добавить CancellationToken для прерывания подъема при получении урона
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

            IsGrounded = true;
        }
    }
}