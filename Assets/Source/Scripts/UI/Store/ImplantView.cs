using ImplantSystem.AttackImplants;
using UnityEngine;

namespace UI.Store
{
    public class ImplantView : MonoBehaviour
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string RuName { get; private set; }
        [field: SerializeField] public string TrName { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite ImplantTypeImage { get; private set; }
        [field: SerializeField] public Sprite ImplantImage { get; private set; }
        [field: SerializeField] public AttackImplant AttackImplant { get; private set; }
    }
}