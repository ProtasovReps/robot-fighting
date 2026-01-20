using System;
using CharacterSystem.CharacterHealth;
using FiniteStateMachine.States;
using HitSystem;
using Interface;
using R3;

namespace CharacterSystem.Dying
{
    public abstract class Death : IDisposable
    {
        private readonly Subject<Unit> _upDeath;
        private readonly Subject<Unit> _downDeath;
        private readonly CompositeDisposable _subscriptions;
        
        protected Death(HitReader hitReader, Health health, IConditionAddable conditionAddable)
        {
            conditionAddable.Add<DeathState>(_ => health.Value.CurrentValue <= 0);

            _subscriptions = new CompositeDisposable();
            _upDeath = new Subject<Unit>();
            _downDeath = new Subject<Unit>();
            
            Subscribe(hitReader.TorsoHitted, _upDeath, health);
            Subscribe(hitReader.LegsHitted, _downDeath, health);
        }

        public Observable<Unit> UpDeath => _upDeath;
        public Observable<Unit> DownDeath => _downDeath;

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
        
        private void Subscribe(Observable<Unit> observable, Subject<Unit> subject, Health health)
        {
            Observable.CombineLatest(observable, health.Value, (hit, healthValue) => (hit, healthValue))
                .Where(tuple => tuple.healthValue <= 0)
                .Subscribe(_ => subject.OnNext(Unit.Default))
                .AddTo(_subscriptions);
        }
    }
}