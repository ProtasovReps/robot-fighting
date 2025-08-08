using System.Collections.Generic;
using AnimationSystem;
using AnimationSystem.Factory;
using CharacterSystem.Data;
using CharacterSystem.FighterParts;
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
        [SerializeField] private HealthView _healthView; // может быть это стоит отделить
        [SerializeField] private FighterPart[] _fighterParts;
        [SerializeField] private AnimatedCharacter _animatedCharacter; // также это
        [SerializeField] private FighterData _fighterData;
        [SerializeField] private HitReader _hitReader;

        protected FighterData Produce(AnimationFactory animationFactory,
            IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            Health health = new(_fighterData.StartHealthValue);
            Dictionary<IAttack, Spherecaster> attacks = new();

            new Hit(_fighterData.StunDuration, _hitReader, conditionAddable);
            new Block(_fighterData.BlockDuration, stateMachine, conditionAddable);

            foreach (AttackData data in _fighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Duration, data.Delay,
                    AttackStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }

            for (int i = 0; i < _fighterParts.Length; i++)
            {
                _fighterParts[i].Initialize(health);
            }

            _hitReader.Initialize(health);
            _attacker.SetAttacks(attacks);
            _healthView.Initialize(health);
            animationFactory.Produce(_animatedCharacter, stateMachine);
            return _fighterData;
        }
    }
}