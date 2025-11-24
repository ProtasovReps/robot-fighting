using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public abstract class ImplantFactory : MonoBehaviour
    {
        private readonly Dictionary<AttackPart, AttackPartSide> _attackSides = new();

        public ImplantPlaceHolderStash Produce()
        {
            AttackImplant[] implants = GetImplants();
            ImplantPlaceHolderStash stash = GetStash();
            
            stash.Initialize(implants.Length);
            
            InstallAttackSides();

            for (int i = 0; i < implants.Length; i++)
            {
                AttackPart requiredPart = implants[i].Parameters.RequiredPart;
                AttackPartSide requiredPartSide = _attackSides[requiredPart];
                ImplantPlaceHolder placeHolder = stash.Get(requiredPartSide);
                AttackImplant newImplant = Instantiate(implants[i]);

                placeHolder.SetImplant(newImplant);
            }

            return stash;
        }

        protected abstract AttackImplant[] GetImplants();
        protected abstract ImplantPlaceHolderStash GetStash();
        protected abstract void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides);

        private void InstallAttackSides()
        {
            _attackSides.Add(AttackPart.UpProjectile, AttackPartSide.UpProjectile);
            _attackSides.Add(AttackPart.DownProjectile, AttackPartSide.DownProjectile);

            AddAttackSides(_attackSides);
        }
    }
}