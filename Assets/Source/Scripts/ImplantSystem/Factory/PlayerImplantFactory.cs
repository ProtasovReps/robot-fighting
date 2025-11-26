using System;
using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using UI.Store;
using UnityEngine;
using YG.Saver;

namespace ImplantSystem.Factory
{
    public class PlayerImplantFactory : ImplantFactory
    {
        [SerializeField] private ImplantView[] _upAttackImplants;
        [SerializeField] private ImplantView[] _downAttackImplants;
        [SerializeField] private ImplantView[] _superAttackImplants;
        
        private ImplantPlaceHolderStash _stash;
        private EquipedImplantSaver _implantSaver;
        
        public void Initialize(ImplantPlaceHolderStash stash, EquipedImplantSaver equipedImplantSaver)
        {
            _stash = stash;
            _implantSaver = equipedImplantSaver;
        }
        
        protected override AttackImplant[] GetImplants()
        {
            AttackImplant upAttackImplant = GetImplant(AttackType.UpAttack, _upAttackImplants);
            AttackImplant downAttackImplant = GetImplant(AttackType.DownAttack, _downAttackImplants);
            AttackImplant superAttackImplant = GetImplant(AttackType.Super, _superAttackImplants);
            
            return new[] { upAttackImplant, downAttackImplant, superAttackImplant};
        }

        protected override ImplantPlaceHolderStash GetStash()
        {
            return _stash;
        }

        protected override void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides)
        {
            partSides.Add(AttackPart.Hands, AttackPartSide.LeftHand);
            partSides.Add(AttackPart.Legs, AttackPartSide.RightLeg);
        }

        private AttackImplant GetImplant(AttackType attackType, ImplantView[] implants)
        {
            for (int i = 0; i < implants.Length; i++)
            {
                if (_implantSaver.IsSetted(attackType, implants[i]) == false)
                    continue;

                return implants[i].AttackImplant;
            }

            throw new ArgumentException(nameof(implants));
        }
    }
}