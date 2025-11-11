using UnityEngine;

namespace UI.Store
{
    [CreateAssetMenu(fileName = "Good", menuName = "Goods/Good")]
    public class GoodView : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite GoodImage { get; private set; }
        [field: SerializeField] public Sprite RarenessImage { get; private set; }
        [field: SerializeField] public Good Good { get; private set; }
    }
}