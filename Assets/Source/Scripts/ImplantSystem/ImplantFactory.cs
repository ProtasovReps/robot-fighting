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
        [SerializeField] private ImplantPlaceHolderStash _placeHolderStash;

        public ImplantPlaceHolderStash Produce()
        {
            _placeHolderStash.Initialize();
            
            for (int i = 0; i < _attackImplants.Length; i++)
            {
                AttackPart requiredPlaceholder = _attackImplants[i].RequiredPart;
                ImplantPlaceHolder placeHolder = _placeHolderStash.Get(requiredPlaceholder);
                AttackImplant newImplant = Instantiate(_attackImplants[i]);
                
                placeHolder.SetImplant(newImplant);
            }

            return _placeHolderStash;
        }
    }
}