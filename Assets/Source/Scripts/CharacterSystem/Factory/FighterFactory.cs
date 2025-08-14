using System.Collections.Generic;
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
        [SerializeField] private FighterData _fighterData;
        [SerializeField] private HitReader _hitReader;
        [SerializeField] private HitImpact _hitImpact;
        
        protected FighterData Produce(IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            Health health = new(_fighterData.StartHealthValue);
            Dictionary<IAttack, Spherecaster> attacks = new();

            new Block(_fighterData.BlockDuration, stateMachine, conditionAddable);

            foreach (AttackData data in _fighterData.Attacks)
            {
                DefaultAttack attack = new(data.Damage, data.Duration, data.Delay,
                    AttackStateFinder.GetState(data.AttackType));

                attacks.Add(attack, data.Spherecaster);
            }

            foreach (FighterPart fighterPart in _fighterParts)
            {
                fighterPart.Initialize(health);
            }

            _hitReader.Initialize();
            _hitImpact.Initialize(_hitReader);

            new UpHit(_fighterData.StunDuration, _hitReader, conditionAddable);
            new DownHit(_fighterData.StunDuration, _hitReader, conditionAddable); // нужно разные Duration

            _attacker.SetAttacks(attacks);
            _healthView.Initialize(health);
            return _fighterData;
        }
    }
}