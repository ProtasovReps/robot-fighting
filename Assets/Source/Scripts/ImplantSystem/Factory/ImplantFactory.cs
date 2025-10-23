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

        [SerializeField] private ImplantPlaceHolderStash _placeHolderStash;

        public ImplantPlaceHolderStash Produce()
        {
            AttackImplant[] implants = GetImplants();

            _placeHolderStash.Initialize(implants.Length);
            
            InstallAttackSides();

            for (int i = 0; i < implants.Length; i++)
            {
                AttackPart requiredPart = implants[i].AttackParameters.RequiredPart;
                AttackPartSide requiredPartSide = _attackSides[requiredPart];
                ImplantPlaceHolder placeHolder = _placeHolderStash.Get(requiredPartSide);
                AttackImplant newImplant = Instantiate(implants[i]);

                placeHolder.SetImplant(newImplant);
            }

            return _placeHolderStash;
        }

        protected abstract AttackImplant[] GetImplants();
        protected abstract void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides);

        private void InstallAttackSides()
        {
            _attackSides.Add(AttackPart.UpProjectile, AttackPartSide.UpProjectile);
            _attackSides.Add(AttackPart.DownProjectile, AttackPartSide.DownProjectile);

            AddAttackSides(_attackSides);
        }
    }
}