using System;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem
{
    public abstract class ImplantPlaceHolder : MonoBehaviour
    {
        public void SetImplant(AttackImplant implant)
        {
            if (IsValidImplant(implant) == false)
                throw new ArgumentException(nameof(implant));
            
            implant.transform.SetParent(transform);
            implant.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public abstract bool IsValidImplant(AttackImplant implant);
    }
}