using System;
using HitSystem;
using Extensions;
using R3;

namespace FightingSystem
{
    public class HitQueueCounter : IDisposable
    {
        private readonly Timer _timer;
        private readonly int _maxHitCount;
        private readonly Subject<Unit> _hitQueueExecuted;
        private readonly IDisposable _subscription;

        private int _hitCount;

        public HitQueueCounter(HitReader hitReader, float maxHitInterval, int maxHitCount)
        {
            if (hitReader == null)
                throw new ArgumentNullException(nameof(hitReader));

            if (maxHitCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxHitCount));

            _timer = new Timer(maxHitInterval);
            _maxHitCount = maxHitCount;
            _hitQueueExecuted = new Subject<Unit>();

            _subscription = hitReader.Hitted
                .Subscribe(_ => Count());
        }

        public Observable<Unit> HitQueueExecuted => _hitQueueExecuted;

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        private void Count()
        {
            if (_timer.IsContinuing == false)
            {
                Reset();
                _timer.Start();
            }

            _hitCount++;

            if (_hitCount == _maxHitCount)
            {
                Reset();
                _hitQueueExecuted.OnNext(Unit.Default);
            }
        }

        private void Reset()
        {
            _hitCount = 0;
        }
    }
}