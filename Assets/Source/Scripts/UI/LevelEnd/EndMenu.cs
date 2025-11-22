using System;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UI.Effect;
using UnityEngine;

namespace UI.LevelEnd
{
    public class EndMenu<TMachine> : MonoBehaviour
    where TMachine : IStateMachine
    {
        [SerializeField] private ScaleAnimation _shadowAnimation;
        [SerializeField] private ScaleAnimation _upgradeAnimation;
        [SerializeField] private float _appearDelay;
        
        private IDisposable _subscription;
        
        [Inject]
        private void Inject(TMachine machine)
        {
            _subscription = machine.Value
                .Delay(TimeSpan.FromSeconds(_appearDelay))
                .Where(value => value is DeathState)
                .Subscribe(_ => Appear());
        }

        protected virtual void Appear()
        {
            _subscription.Dispose();
            _shadowAnimation.Play().Forget();
            _upgradeAnimation.Play().Forget();
        }
    }
}