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
        protected FighterData Produce(FighterData fighterData, AnimationFactory animationFactory, IStateMachine stateMachine)
        {
            Health health = new(fighterData.StartHealthValue);
            Stun stun = new(fighterData.StunDuration, health);
            Block block = new(fighterData.BlockDuration, stateMachine);
            Dictionary<IAttack, Spherecaster> attacks = new();
            
            foreach (AttackData data in fighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Duration, data.Delay, AttackStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }
            
            fighterData.Attacker.SetAttacks(attacks);
            fighterData.HealthView.Initialize(health);
            fighterData.Fighter.Initialize(health, stun, block);
            
            animationFactory.Produce(fighterData.AnimatedCharacter, stateMachine);
            return fighterData;
        }
    }
}