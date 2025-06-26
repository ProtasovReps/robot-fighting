using AnimationSystem;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using FiniteStateMachine.States;
using HealthSystem;
using Interface;
using MovementSystem;
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
            DefaultAttack fistAttack = new(10, 0.2f);
            
            CharacterAnimation[] animations =
            {
                new TriggerAnimation<IdleState>(_stateMachine, _fighter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<MoveLeftState>(_stateMachine, _fighter.Animator, AnimationHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(_stateMachine, _fighter.Animator, AnimationHashes.MoveRight),
                new TriggerAnimation<PunchState>(_stateMachine, _fighter.Animator, AnimationHashes.Attack),
                new TriggerAnimation<JumpState>(_stateMachine, _fighter.Animator, AnimationHashes.Jump)
            };

            _attacker.Initialize(fistAttack);
            _fighter.Initialize(health, animations);
            _healthView.Initialize(health);
            return _fighter;
        }
    }
}