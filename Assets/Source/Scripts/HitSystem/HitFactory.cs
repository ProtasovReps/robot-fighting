using ArmorSystem;
using CharacterSystem.Data;
using FightingSystem;
using HealthSystem;
using HitSystem.FighterParts;
using Interface;
using UnityEngine;

namespace HitSystem
{
    public class HitFactory : MonoBehaviour
    {
        [SerializeField] private HitCollider _upHitCollider; // их получать из сейва SkinInfo
        [SerializeField] private HitCollider _downHitCollider;
        [SerializeField] private HitReader _hitReader;
        [SerializeField] private HitImpact _hitImpact;
        [SerializeField] private FighterData _fighterData;

        public HitReader Produce(Health health, IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            var torso = new Torso(health);
            var legs = new Legs(health);
            var torsoArmor = new NoTorsoArmor(torso); // армор получаем из сейва или инвентаря, придумать
            var legsArmor = new NoLegsArmor(legs);
            var block = new Block(_fighterData.BlockDuration, _fighterData.BlockValue, torsoArmor, stateMachine, conditionAddable);//

            _upHitCollider.Initialize(block);
            _downHitCollider.Initialize(legsArmor);
            
            _hitReader.Initialize(torso, legs);
            _hitImpact.Initialize(_hitReader);

            new UpHit(_fighterData.StunDuration, _hitReader, conditionAddable);
            new DownHit(_fighterData.DownStunDuration, _hitReader, conditionAddable);
            
            return _hitReader;
        }
    }
}