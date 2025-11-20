using System;
using Cysharp.Threading.Tasks;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UI.Effect;
using UnityEngine;

namespace UI.VictoryMenu
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _shadowAnimation;
        [SerializeField] private ScaleAnimation _upgradeAnimation;
        [SerializeField] private float _appearDelay;
        
        private IDisposable _subscription;

        [Inject]
        private void Inject(BotStateMachine botStateMachine)
        {
            _subscription = botStateMachine.Value
                .Delay(TimeSpan.FromSeconds(_appearDelay))
                .Where(value => value is DeathState)
                .Subscribe(_ => Appear());
        }

        private void Appear()
        {
            _subscription.Dispose();
            _shadowAnimation.Play().Forget();
            _upgradeAnimation.Play().Forget();
        }
    }
}