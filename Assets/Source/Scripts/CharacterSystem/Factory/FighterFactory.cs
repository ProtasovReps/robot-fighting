using System.Collections.Generic;
using AnimationSystem;
using CharacterSystem.Data;
using Extensions;
using FightingSystem.Attacks;
using HealthSystem;
using Interface;

namespace CharacterSystem.Factory
{
    public abstract class FighterFactory
    {
        protected FighterFactory(FighterData data, IStateMachine stateMachine)
        {
            FighterData = data;
            StateMachine = stateMachine;
        }
        
        protected FighterData FighterData { get; }
        protected IStateMachine StateMachine { get; }

        public void Produce()
        {
            Health health = new(FighterData.StartHealthValue);
            Dictionary<IAttack, Spherecaster> attacks = new();

            foreach (AttackData data in FighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Delay, RequiredStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }

            CharacterAnimation[] animations = GetAnimations();
            
            FighterData.Attacker.Initialize(attacks);
            FighterData.Fighter.Initialize(health, animations);
            FighterData.HealthView.Initialize(health);
        }

        protected abstract CharacterAnimation[] GetAnimations();
    }
}