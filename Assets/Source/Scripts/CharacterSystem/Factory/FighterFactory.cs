using System.Collections.Generic;
using AnimationSystem;
using AnimationSystem.Factory;
using CharacterSystem.Data;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using HealthSystem;
using Interface;
using UnityEngine;

namespace CharacterSystem.Factory
{
    public abstract class FighterFactory : MonoBehaviour
    {
        [SerializeField] private Attacker _attacker;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Fighter _fighter;
        [SerializeField] private AnimatedCharacter _animatedCharacter;
        [SerializeField] private FighterData _fighterData;
        
        protected FighterData Produce(AnimationFactory animationFactory,
            IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            Health health = new(_fighterData.StartHealthValue);
            Dictionary<IAttack, Spherecaster> attacks = new();
            
            new Stun (_fighterData.StunDuration, health, conditionAddable);
            new Block(_fighterData.BlockDuration, stateMachine, conditionAddable);

            foreach (AttackData data in _fighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Duration, data.Delay,
                    AttackStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }

            _attacker.SetAttacks(attacks);
            _healthView.Initialize(health);
            _fighter.Initialize(health);
            animationFactory.Produce(_animatedCharacter, stateMachine);
            return _fighterData;
        }
    }
}