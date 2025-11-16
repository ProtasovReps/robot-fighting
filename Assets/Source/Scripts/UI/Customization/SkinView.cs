using CharacterSystem;
using UnityEngine;

namespace UI.Customization
{
    [CreateAssetMenu(fileName = "SkinView", menuName = "GoodView/SkinView")]
    public class SkinView : ScriptableObject
    {
        [field: SerializeField] public Fighter Fighter { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}