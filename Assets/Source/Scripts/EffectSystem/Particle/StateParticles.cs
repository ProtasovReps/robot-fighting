using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace EffectSystem.Particle
{
    public class StateParticles<TMachine, TState> : MonoBehaviour
        where TMachine : IStateMachine
        where TState : State
    {
        [SerializeField] private ParticleSystem[] _effects;

        [Inject]
        private void Inject(TMachine machine)
        {
            machine.Value
                .Where(value => value is TState)
                .Subscribe(_ => Play())
                .AddTo(this);
        }

        private void Play()
        {
            for (int i = 0; i < _effects.Length; i++)
            {
                _effects[i].Play();
            }
        }
    }
}