using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public abstract class HitImpact : MonoBehaviour // убрать в fightingSystem
    {
        [SerializeField] private float _targetRightPosition;
        [SerializeField] private float _impactTime;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        protected void Initialize(IStateMachine stateMachine)
        {
            stateMachine.Value
                .Where(state => state.Type == typeof(HittedState))
                .Subscribe(_ => Impact().Forget())
                .AddTo(this);
        }

        private async UniTaskVoid Impact()
        {
            Vector3 targetPosition = _transform.position + Vector3.right * _targetRightPosition;
            float expiredTime = 0f;

            while (expiredTime < _impactTime)
            {
                float newPosition = Mathf.Lerp(_transform.position.x, targetPosition.x, expiredTime / _impactTime);

                _transform.position = new Vector3(newPosition, _transform.position.y, _transform.position.z);
                expiredTime += Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}