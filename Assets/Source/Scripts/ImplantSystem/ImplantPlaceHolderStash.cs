using System;
using System.Collections.Generic;
using Extensions;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace ImplantSystem
{
    public class ImplantPlaceHolderStash : MonoBehaviour
    {
        [SerializeField] private ImplantPlaceHolder[] _implantPlaceHolders;
        
        private Dictionary<AttackPart, ImplantPlaceHolder> _placeHolderParts;

        public IEnumerable<ImplantPlaceHolder> PlaceHolders => _implantPlaceHolders;
        
        public void Initialize()
        {
            _placeHolderParts = new Dictionary<AttackPart, ImplantPlaceHolder>();

            for (int i = 0; i < _implantPlaceHolders.Length; i++)
            {
                ImplantPlaceHolder placeHolder = _implantPlaceHolders[i];

                if (_placeHolderParts.ContainsKey(placeHolder.AttackPart))
                {
                    throw new ArgumentOutOfRangeException(nameof(placeHolder));
                }
                
                _placeHolderParts.Add(placeHolder.AttackPart, placeHolder);
            }
        }

        public ImplantPlaceHolder Get(AttackPart requiredPart)
        {
            if (_placeHolderParts.ContainsKey(requiredPart) == false)
            {
                throw new ArgumentException(nameof(requiredPart));
            }

            return _placeHolderParts[requiredPart];
        }
    }
}