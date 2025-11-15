using ImplantSystem.AttackImplants;
using UnityEngine;

namespace UI.Store
{
    [CreateAssetMenu(fileName = "ImplantView", menuName = "GoodView/ImplantView")]
    public class ImplantView : SellableView
    {
        [field: SerializeField] public AttackImplant AttackImplant { get; private set; }
    }
}