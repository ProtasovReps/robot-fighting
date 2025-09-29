using UnityEngine;

namespace HitSystem
{
    public class HitColliderStash : MonoBehaviour
    {
        [field: SerializeField] public HitCollider UpCollider { get; private set; }
        [field: SerializeField] public HitCollider DownCollider { get; private set; }
    }
}
