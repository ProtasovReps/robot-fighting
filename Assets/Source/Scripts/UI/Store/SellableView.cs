using UnityEngine;

namespace UI.Store
{
    public abstract class SellableView : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite SellableImage { get; private set; }
    }
}