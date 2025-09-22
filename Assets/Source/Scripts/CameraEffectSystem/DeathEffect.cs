using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace CameraEffectSystem
{
    public class DeathEffect : MonoBehaviour // очень похожи с CameraEffect, поэтому можно объединить под одним родителем
    {
        [SerializeField, Range(0.1f, 1f)] private float _targetTimeScale; 
        
        [Inject]
        private void Inject(BotStateMachine botStateMachine, PlayerStateMachine playerStateMachine)
        {
            Observable<State> botHitted = botStateMachine.Value
                .Where(value => value is DeathState); 
            
            Observable<State> playerHitted = playerStateMachine.Value
                .Where(value => value is DeathState);

            Observable.Merge(botHitted, playerHitted)
                .Subscribe(_ => Apply())
                .AddTo(this);
        }

        private void Apply()
        {
            Time.timeScale = _targetTimeScale;
        }
    }
}