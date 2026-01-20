using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.PlaceHolders
{
    public class ImplantPlaceHolder : MonoBehaviour
    {
        private readonly List<AttackImplant> _implants = new ();
        
        [field: SerializeField] public AttackPartSide AttackPartSide { get; private set; }     

        public IEnumerable<AttackImplant> Implants => _implants;
        
        public void SetImplant(AttackImplant implant)
        {
            implant.transform.SetParent(transform);
            implant.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            _implants.Add(implant);
        }
    }
}