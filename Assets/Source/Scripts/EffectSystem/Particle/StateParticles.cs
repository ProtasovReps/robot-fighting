using FiniteStateMachine.States;
using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace EffectSystem.StateDependent
{
    public class StateParticles<TMachine, KState> : MonoBehaviour
        where TMachine : IStateMachine
        where KState : State
    {
        [SerializeField] private ParticleSystem[] _effects;

        [Inject]
        private void Inject(TMachine machine)
        {
            machine.Value
                .Where(value => value is KState)
                .Subscribe(_ => Play())
                .AddTo(this);
        }

        private void Play()
        {
            for (int i = 0; i < _effects.Length; i++)
                _effects[i].Play();
        }
    }
}