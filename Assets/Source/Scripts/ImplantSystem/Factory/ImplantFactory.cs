using CharacterSystem.Data;
using Extensions;
using ImplantSystem.AttackImplants;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public abstract class ImplantFactory : MonoBehaviour
    {
        public ImplantPlaceHolderStash Produce()
        {
            ImplantPlaceHolderStash stash = GetPlaceholderStash();
            AttackImplant[] implants = GetImplants();
            
            stash.Initialize();
            
            for (int i = 0; i < implants.Length; i++)
            {
                AttackPart requiredPart = implants[i].RequiredPart;
                ImplantPlaceHolder placeHolder = stash.Get(requiredPart);
                AttackImplant newImplant = Instantiate(implants[i]);
                
                placeHolder.SetImplant(newImplant);
            }

            return stash;
        }

        protected abstract AttackImplant[] GetImplants();
        protected abstract ImplantPlaceHolderStash GetPlaceholderStash();
    }
}