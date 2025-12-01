using R3;
using UnityEngine;

namespace UI.Guide.Pointers
{
    public class AppearingPointer : MonoBehaviour
    {
        [SerializeField] private DisappearingPointer _disappearingPointer;

        private void Awake()
        {
            _disappearingPointer.Disappeared
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);
            
            gameObject.SetActive(false);
        }
    }
}