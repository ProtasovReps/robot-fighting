using System;
using FiniteStateMachine;
using FiniteStateMachine.States;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace YG
{
    public class ReviewShower : MonoBehaviour
    {
        [SerializeField] private float _delay;
        
        [Inject]
        private void Inject(BotStateMachine machine)
        {
            machine.Value
                .Delay(TimeSpan.FromSeconds(_delay))
                .Where(state => state is DeathState)
                .Subscribe(_ => YG2.ReviewShow())
                .AddTo(this);
        }
    }
}