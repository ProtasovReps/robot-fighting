using UI.Guide.Pointers;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Guide
{
    public class TraderGuide : MonoBehaviour
    {
        [SerializeField] private DisappearingPointer _disappearingPointer;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Button[] _disablingButtons;

        private void Awake()
        {
            if (YG2.saves.IsGuidePassed)
            {
                _scrollRect.vertical = true;
                return;
            }

            _disappearingPointer.gameObject.SetActive(true);
            _scrollRect.vertical = false;

            for (int i = 0; i < _disablingButtons.Length; i++)
            {
                _disablingButtons[i].interactable = false;
            }
        }
    }
}