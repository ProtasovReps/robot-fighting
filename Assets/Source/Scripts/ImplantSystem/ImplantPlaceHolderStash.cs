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
        
        private Dictionary<AttackPartSide, ImplantPlaceHolder> _placeHolderSides;
        private List<ImplantPlaceHolder> _activePlaceHolders;
        
        public IEnumerable<ImplantPlaceHolder> ActivePlaceHolders => _activePlaceHolders;
        
        public void Initialize(int requiredHoldersCount)
        {
            _activePlaceHolders = new List<ImplantPlaceHolder>(requiredHoldersCount);
            _placeHolderSides = new Dictionary<AttackPartSide, ImplantPlaceHolder>();
            
            for (int i = 0; i < _implantPlaceHolders.Length; i++)
            {
                ImplantPlaceHolder placeHolder = _implantPlaceHolders[i];

                if (_placeHolderSides.ContainsKey(placeHolder.AttackPartSide))
                {
                    throw new ArgumentOutOfRangeException(nameof(placeHolder));
                }
                
                _placeHolderSides.Add(placeHolder.AttackPartSide, placeHolder);
            }
        }

        public ImplantPlaceHolder Get(AttackPartSide requiredPartSide)
        {
            if (_placeHolderSides.ContainsKey(requiredPartSide) == false)
            {
                throw new ArgumentException(nameof(requiredPartSide));
            }

            ImplantPlaceHolder holder = _placeHolderSides[requiredPartSide];
            
            AddActiveHolder(holder);
            return holder;
        }

        private void AddActiveHolder(ImplantPlaceHolder placeHolder)
        {
            if (_activePlaceHolders.Contains(placeHolder))
            {
                return;
            }
            
            _activePlaceHolders.Add(placeHolder);
        }
    }
}