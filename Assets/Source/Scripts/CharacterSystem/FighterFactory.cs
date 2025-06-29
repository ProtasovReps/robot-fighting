using System.Collections.Generic;
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
        [SerializeField] private HealthView _healthView; 
        [SerializeField] [Min(1)] private float _startHealthValue;
        [SerializeField] private Attacker _attacker; 
        [SerializeField] private AttackData[] _attacks;
        
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
            Dictionary<IAttack, Spherecaster> attacks = new();

            for (int i = 0; i < _attacks.Length; i++)
            {
                AttackData data = _attacks[i];
                DefaultAttack attack = new(data.Damage, data.Delay, RequiredStateFinder.GetState(data.AttackType));
                
                attacks.Add(attack, data.Spherecaster);
            }
            
            CharacterAnimation[] animations =
            {
                new TriggerAnimation<IdleState>(_stateMachine, _fighter.Animator, AnimationHashes.Idle),
                new TriggerAnimation<JumpState>(_stateMachine, _fighter.Animator, AnimationHashes.Jump),
                new TriggerAnimation<MoveLeftState>(_stateMachine, _fighter.Animator, AnimationHashes.MoveLeft),
                new TriggerAnimation<MoveRightState>(_stateMachine, _fighter.Animator, AnimationHashes.MoveRight),
                new TriggerAnimation<PunchState>(_stateMachine, _fighter.Animator, AnimationHashes.ArmAttack),
                new TriggerAnimation<KickState>(_stateMachine, _fighter.Animator, AnimationHashes.LegAttack)
            };

            _attacker.Initialize(attacks);
            _fighter.Initialize(health, animations);
            _healthView.Initialize(health);
            return _fighter;
        }
    }
}