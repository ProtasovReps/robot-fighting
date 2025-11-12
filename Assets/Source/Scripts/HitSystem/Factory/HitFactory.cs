using CharacterSystem.Parameters;
using Extensions;
using FightingSystem;
using HealthSystem;
using HitSystem.FighterParts;
using Interface;
using UnityEngine;

namespace HitSystem
{
    public abstract class HitFactory : MonoBehaviour
    {
        [SerializeField] private HitReader _hitReader;
        [SerializeField] private HitImpact _hitImpact;
        [SerializeField] private FighterParameters _fighterParameters;
        [SerializeField] private ColliderSwitcher _colliderSwitcher;
        
        public HitReader Produce(Health health, IStateMachine machine, IConditionAddable conditionAddable)
        {
            Torso torso = new(health);
            Legs legs = new(health);
            float blockValue = GetBlockValue();
            Block block = new(_fighterParameters.BlockDuration, blockValue, torso, machine, conditionAddable);

            InitializeColliders(block, legs, machine);
            InitializeHit(conditionAddable, torso, legs);
            return _hitReader;
        }

        protected abstract float GetBlockValue();
        
        protected abstract HitColliderStash GetColliderStash();

        private void InitializeHit(IConditionAddable conditionAddable, Torso torso, Legs legs)
        {
            _hitReader.Initialize(torso, legs);
            _hitImpact.Initialize(_hitReader);

            new UpHit(_fighterParameters.StunDuration, _hitReader, conditionAddable);
            new DownHit(_fighterParameters.DownStunDuration, _hitReader, conditionAddable);
        }

        private void InitializeColliders(Block block, Legs legs, IStateMachine stateMachine)
        {
            HitColliderStash stash = GetColliderStash();

            stash.UpCollider.Initialize(block);
            stash.DownCollider.Initialize(legs);
            
            _colliderSwitcher.Initialize(stateMachine, stash);
        }
    }
}