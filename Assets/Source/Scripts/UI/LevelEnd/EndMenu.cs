using System;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UI.Effect;
using UnityEngine;
using YG;

namespace UI.LevelEnd
{
    public abstract class EndMenu<TMachine> : MonoBehaviour
        where TMachine : IStateMachine
    {
        [SerializeField] private ScaleAnimation _shadowAnimation;
        [SerializeField] private ScaleAnimation _upgradeAnimation;
        [SerializeField] private float _appearDelay;
        [SerializeField] private LeaderboardYG _leaderboard;
        
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
            int currentScore = YG2.saves.GloryPoints;
            int newScore = GetNewScore(currentScore);

            YG2.saves.GloryPoints = newScore;

            _leaderboard.SetLeaderboard(newScore);
            _leaderboard.UpdateLB();

            _subscription.Dispose();
            _shadowAnimation.Play().Forget();
            _upgradeAnimation.Play().Forget();
        }

        protected abstract int GetNewScore(int currentScore);
    }
}