using ArmorSystem;
using CharacterSystem.Parameters;
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

        public HitReader Produce(Health health, IStateMachine machine, IConditionAddable conditionAddable)
        {
            Torso torso = new(health);
            Legs legs = new(health);
            (Armor<Torso>, Armor<Legs>) armor = GetArmor(torso, legs);

            Block block = new(_fighterParameters.BlockDuration, _fighterParameters.BlockValue,
                armor.Item1, machine, conditionAddable);

            InitializeColliders(block, armor.Item2);
            InitializeHit(conditionAddable, torso, legs);
            return _hitReader;
        }

        protected abstract Armor<Torso> GetTorsoArmor();
        protected abstract Armor<Legs> GetLegsArmor();
        protected abstract HitColliderStash GetColliderStash();

        private (Armor<Torso> torsoArmor, Armor<Legs> legArmor) GetArmor(Torso torso, Legs legs)
        {
            Armor<Torso> torsoArmor = GetTorsoArmor();
            Armor<Legs> legArmor = GetLegsArmor();

            torsoArmor.Initialize(torso);
            legArmor.Initialize(legs);
            return (torsoArmor, legArmor);
        }

        private void InitializeHit(IConditionAddable conditionAddable, Torso torso, Legs legs)
        {
            _hitReader.Initialize(torso, legs);
            _hitImpact.Initialize(_hitReader);

            new UpHit(_fighterParameters.StunDuration, _hitReader, conditionAddable);
            new DownHit(_fighterParameters.DownStunDuration, _hitReader, conditionAddable);
        }

        private void InitializeColliders(Block block, Armor<Legs> legArmor)
        {
            HitColliderStash stash = GetColliderStash();

            stash.UpCollider.Initialize(block);
            stash.DownCollider.Initialize(legArmor);
        }
    }
}