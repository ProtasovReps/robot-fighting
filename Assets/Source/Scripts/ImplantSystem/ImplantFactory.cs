using CharacterSystem.Data;
using Extensions;
using ImplantSystem.AttackImplants;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace ImplantSystem
{
    public class ImplantFactory : MonoBehaviour
    {
        [Header("Потом из сейва получать")]
        [SerializeField] private AttackImplant[] _attackImplants;
        [SerializeField] private FighterData _fighterData; // получать из сейва SkinInfo

        public ImplantPlaceHolderStash Produce()
        {
            ImplantPlaceHolderStash stash = _fighterData.SkinData.PlaceholderStash;
            
            stash.Initialize();
            
            for (int i = 0; i < _attackImplants.Length; i++)
            {
                AttackPart requiredPart = _attackImplants[i].RequiredPart;
                ImplantPlaceHolder placeHolder = stash.Get(requiredPart);
                AttackImplant newImplant = Instantiate(_attackImplants[i]);
                
                placeHolder.SetImplant(newImplant);
            }

            return stash;
        }
    }
}