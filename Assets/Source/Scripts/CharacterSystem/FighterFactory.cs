using AnimationSystem;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using FiniteStateMachine.States;
using HealthSystem;
using Interface;
using Reflex.Attributes;
using UnityEngine;

namespace CharacterSystem
{
    //Возвращать в случае чего файтера, сама фабрика должна быть Fighter, а враг или игрок - наследники переопределят
    public class FighterFactory : MonoBehaviour
    {
        [SerializeField] private Fighter _fighter; // А если скины менять, т.е. модельки? ScriptableObject?
        [SerializeField] private HealthView _healthView; // получать из файтера
        [SerializeField] [Min(1)] private float _startHealthValue; // скриптэбл обджект?
        [SerializeField] private Attacker _attacker; // временно, получать нужно иначе. 

        private IStateChangeable _stateMachine;

        [Inject]
        private void Inject(IStateChangeable stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Awake() // точно не тут, скорее билдер уровня должен этим заниматься
        {
            Produce();
        }

        private Fighter Produce()
        {
            Health health = new(_startHealthValue);
            DefaultAttack fistAttack = new(10, 0.5f);
            DefaultAttack legAttack = new(5, 0.5f); //
            CharacterAnimation[] animations =
            {
                new TriggerAnimation<IdleState>(_stateMachine, _fighter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<MoveState>(_stateMachine, _fighter.Animator, AnimationHashes.Move),
            };

            _attacker.Initialize(fistAttack);
            _fighter.Initialize(health, animations);
            _healthView.Initialize(health);
            return _fighter;
        }
    }
}