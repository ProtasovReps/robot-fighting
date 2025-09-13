using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;
using UnityEngine;

namespace Extensions
{
    public class Timer : IContinuous
    {
        private readonly float _targetTime;
        private readonly Subject<Unit> _finallized;        
        
        private CancellationTokenSource _cancellationTokenSource;

        public Timer(float targetTime)
        {
            if (targetTime <= 0f)
                throw new ArgumentOutOfRangeException(nameof(targetTime));

            _finallized = new Subject<Unit>();
            _targetTime = targetTime;
        }

        public bool IsContinuing { get; private set; }
        public Observable<Unit> Finallized => _finallized;
        
        public void Start()
        {
            Cancel();
            Tick().Forget();
        }
        
        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            IsContinuing = false;
        }
        
        private async UniTaskVoid Tick()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            float expiredTime = 0f;
            IsContinuing = true;

            while (expiredTime < _targetTime && _cancellationTokenSource.IsCancellationRequested == false)
            {
                expiredTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }

            _finallized.OnNext(Unit.Default);
            Cancel();
        }
    }
}