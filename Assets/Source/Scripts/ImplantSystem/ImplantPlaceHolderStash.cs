using System;
using System.Collections.Generic;
using Extensions;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace ImplantSystem
{
    public class ImplantPlaceHolderStash : MonoBehaviour
    {
        [SerializeField] private HandImplantPlaceHolder _handImplantPlaceHolder;
        [SerializeField] private LegImplantPlaceHolder _legImplantPlaceHolder;

        private Dictionary<AttackPart, ImplantPlaceHolder> _placeHolders;

        public IEnumerable<ImplantPlaceHolder> PlaceHolders => _placeHolders.Values;

        public void Initialize()
        {
            _placeHolders = new Dictionary<AttackPart, ImplantPlaceHolder>()
            {
                { AttackPart.Hands, _handImplantPlaceHolder },
                { AttackPart.Legs, _legImplantPlaceHolder }
            };
        }

        public ImplantPlaceHolder Get(AttackPart requiredPart)
        {
            if (_placeHolders.ContainsKey(requiredPart) == false)
                throw new ArgumentException(nameof(requiredPart));

            return _placeHolders[requiredPart];
        }
    }
}