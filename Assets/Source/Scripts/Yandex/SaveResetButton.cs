using R3;
using UI.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YG
{
    public class SaveResetButton : MonoBehaviour
    {
        [SerializeField] private UnitButton _unitButton;

        private void Awake()
        {
            _unitButton.Pressed.Subscribe(_ => EgorLetov()).AddTo(this);
        }

        private void EgorLetov()
        {
            YG2.SetDefaultSaves();
            YG2.SaveProgress();
            SceneManager.LoadScene(3);
        }
    }
}