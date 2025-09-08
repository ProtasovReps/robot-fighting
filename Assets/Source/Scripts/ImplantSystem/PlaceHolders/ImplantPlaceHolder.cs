using System.Collections.Generic;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.PlaceHolders
{
    public abstract class ImplantPlaceHolder : MonoBehaviour
    {
        private readonly List<AttackImplant> _implants = new();

        public IEnumerable<AttackImplant> Implants => _implants;
        
        public void SetImplant(AttackImplant implant)
        {
            implant.transform.SetParent(transform);
            implant.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            _implants.Add(implant);
        }
    }
}