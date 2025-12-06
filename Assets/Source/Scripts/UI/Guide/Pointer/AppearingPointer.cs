using R3;
using UnityEngine;
using YG;

namespace UI.Guide.Pointers
{
    public class AppearingPointer : MonoBehaviour
    {
        [SerializeField] private DisappearingPointer _disappearingPointer;

        private void Awake()
        {
            gameObject.SetActive(false);

            if (YG2.saves.IsGuidePassed)
            {
                return;
            }
            
            _disappearingPointer.Disappeared
                .Subscribe(_ => gameObject.SetActive(true))
                .AddTo(this);
        }
    }
}