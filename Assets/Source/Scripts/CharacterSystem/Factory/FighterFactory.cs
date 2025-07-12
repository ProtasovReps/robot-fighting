using System;
using System.Collections.Generic;
using AnimationSystem.Factory;
using CharacterSystem.Data;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using HealthSystem;
using Interface;

namespace CharacterSystem.Factory
{
    public abstract class FighterFactory
    {
        private readonly FighterData _fighterData;
        
        protected FighterFactory(FighterData fighterData)
        {
            if (fighterData == null)
                throw new ArgumentNullException(nameof(fighterData));

            _fighterData = fighterData;
        }
        
        public IStateMachine Produce(AnimationFactory animationFactory)
        {
            Health health = new(_fighterData.StartHealthValue);
            Stun stun = new(_fighterData.HitDuration, health);
            
            Dictionary<IAttack, Spherecaster> attacks = new();
            
            foreach (AttackData data in _fighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Delay, AttackStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }
            
            _fighterData.Attacker.Initialize(attacks);
            _fighterData.Fighter.Initialize(health, stun);
            _fighterData.HealthView.Initialize(health);

            IStateMachine stateMachine = GetStateMachine();
            
            animationFactory.Produce(_fighterData.AnimatedCharacter, stateMachine);
            return stateMachine;
        }

        protected abstract IStateMachine GetStateMachine();
    }
}