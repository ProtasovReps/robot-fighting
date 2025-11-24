using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using UnityEngine;
using YG.Saver;

namespace ImplantSystem.Factory
{
    public class PlayerImplantFactory : ImplantFactory 
    {
        [SerializeField] private AttackImplant _defaultUpAttackImplant;
        [SerializeField] private AttackImplant _defaultDownAttackImplant;
        [SerializeField] private AttackImplant _defaultSuperAttackImplant;

        protected override AttackImplant[] GetImplants()
        {
            PlayerImplantSave implantSave = new();
            
            if(implantSave.Exists(AttackType.UpAttack) == false)
                InstallDefaultImplants(implantSave);

            AttackImplant upAttackImplant = implantSave.Get(AttackType.UpAttack);
            AttackImplant downAttackImplant = implantSave.Get(AttackType.DownAttack);
            AttackImplant superAttackImplant = implantSave.Get(AttackType.Super);
            
            return new[] { upAttackImplant, downAttackImplant, superAttackImplant};
        }

        protected override void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides)
        {
            partSides.Add(AttackPart.Hands, AttackPartSide.LeftHand);
            partSides.Add(AttackPart.Legs, AttackPartSide.RightLeg);
        }

        private void InstallDefaultImplants(PlayerImplantSave implantSave)
        {
            implantSave.Set(AttackType.UpAttack, _defaultUpAttackImplant);
            implantSave.Set(AttackType.DownAttack, _defaultDownAttackImplant);
            implantSave.Set(AttackType.Super, _defaultSuperAttackImplant);
            
            implantSave.Save();
        }
    }
}