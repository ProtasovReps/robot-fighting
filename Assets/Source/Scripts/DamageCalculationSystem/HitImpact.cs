using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace DamageCalculationSystem
{
    public class HitImpact : MonoBehaviour // также может не быть монобехом
    {
        [SerializeField] private float _targetRightPosition; // разная сила в зависимости от урона?
        [SerializeField] private float _impactTime;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Initialize(HitReader hitReader)
        {
            hitReader.Hitted
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