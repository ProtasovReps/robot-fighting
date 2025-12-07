using CharacterSystem.Parameters;
using Extensions;
using FightingSystem;
using CharacterSystem.CharacterHealth;
using HitSystem.FighterParts;
using HitSystem.HitTypes;
using Interface;
using UnityEngine;

namespace HitSystem.Factory
{
    public abstract class HitFactory : MonoBehaviour
    {
        [SerializeField] private HitReader _hitReader;
        [SerializeField] private HitImpact _hitImpact;
        [SerializeField] private FighterParameters _fighterParameters;
        
        public HitReader Produce(
            Health health,
            IStateMachine machine,
            IConditionAddable conditionAddable,
            Disposer disposer)
        {
            Torso torso = new(health);
            Legs legs = new(health);
            float blockValue = GetBlockValue();
            Block block = new(_fighterParameters.BlockDuration, blockValue, torso, machine, conditionAddable);

            disposer.Add(block);
            
            InitializeColliders(block, legs);
            InitializeHit(conditionAddable, torso, legs, disposer);
            return _hitReader;
        }

        protected abstract float GetBlockValue();
        
        protected abstract HitColliderStash GetColliderStash();

        private void InitializeHit(IConditionAddable conditionAddable, Torso torso, Legs legs, Disposer disposer)
        {
            _hitReader.Initialize(torso, legs);
            _hitImpact.Initialize(_hitReader);

            disposer.Add(new UpHit(_fighterParameters.StunDuration, _hitReader, conditionAddable));
            disposer.Add(new DownHit(_fighterParameters.DownStunDuration, _hitReader, conditionAddable));
        }

        private void InitializeColliders(Block block, Legs legs)
        {
            HitColliderStash stash = GetColliderStash();

            stash.UpCollider.Initialize(block);
            stash.DownCollider.Initialize(legs);
        }
    }
}