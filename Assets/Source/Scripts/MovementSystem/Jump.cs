using Cysharp.Threading.Tasks;
using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class Jump : MonoBehaviour, IContinuous
    {
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _fallSpeed;

        private float _startHeight;
        private Transform _transform;
        private IStateMachine _stateMachine;

        public bool IsContinuing { get; private set; }

        [Inject]
        private void Inject(PlayerStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            _stateMachine = stateMachine;

            conditionAddable.Add<JumpState>(_ => IsContinuing);
        }

        private void Start()
        {
            _transform = transform;
            _startHeight = _transform.position.y;
            _stateMachine.Value
                .Where(state => state.Type == typeof(JumpState))
                .Subscribe(_ => OnJumpPressed())
                .AddTo(this);
        }

        private void OnJumpPressed()
        {
            if (IsContinuing)
            {
                return;
            }

            IsContinuing = true;
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

            IsContinuing = false;
        }
    }
}