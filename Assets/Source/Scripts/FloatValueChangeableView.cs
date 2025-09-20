using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class FloatValueChangeableView<T> : MonoBehaviour // пространство имен
    where T : IFloatValueChangeable
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _animationTime;

    private CancellationTokenSource _tokenSource;
    private Subject<Unit> _filled;

    public Observable<Unit> Filled => _filled;
    
    [Inject]
    private void Inject(T valueChangeable)
    {
        _filled = new Subject<Unit>();
        _slider.maxValue = valueChangeable.MaxValue;
        
        valueChangeable.Value
            .Subscribe(Animate)
            .AddTo(this);

        valueChangeable.Value
            .Where(value => Mathf.Approximately(value, valueChangeable.MaxValue))
            .Subscribe(_ => _filled.OnNext(Unit.Default))
            .AddTo(this);
    }

    private void OnDestroy()
    {
        Cancel();
    }

    private void Animate(float newValue)
    {
        Cancel();

        _tokenSource = new CancellationTokenSource();

        UpdateAnimated(newValue, _tokenSource.Token).Forget();
    }

    private async UniTaskVoid UpdateAnimated(float newValue, CancellationToken token)
    {
        float elapsedTime = 0f;
        float startValue = _slider.value;

        while (Mathf.Approximately(_slider.value, newValue) == false && token.IsCancellationRequested == false)
        {
            _slider.value = Mathf.Lerp(startValue, newValue, elapsedTime / _animationTime);
            elapsedTime += Time.deltaTime;
            await UniTask.Yield(cancellationToken: token, cancelImmediately: true);
        }
    }

    private void Cancel()
    {
        _tokenSource?.Cancel();
    }
}