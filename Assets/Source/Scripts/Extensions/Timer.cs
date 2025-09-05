using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
    public class Timer
    {
        private readonly float _targetTime;

        private CancellationTokenSource _cancellationTokenSource;

        public Timer(float targetTime)
        {
            if (targetTime <= 0f)
                throw new ArgumentOutOfRangeException(nameof(targetTime));

            _targetTime = targetTime;
        }

        public bool IsGoing { get; private set; }

        public void Restart()
        {
            Cancel();
            Tick().Forget();
        }

        private async UniTaskVoid Tick()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            float expiredTime = 0f;
            IsGoing = true;

            while (expiredTime < _targetTime && _cancellationTokenSource.IsCancellationRequested == false)
            {
                expiredTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }

            Cancel();
        }

        private void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            IsGoing = false;
        }
    }
}