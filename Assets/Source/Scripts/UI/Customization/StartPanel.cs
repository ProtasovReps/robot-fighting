using R3;
using UI.Buttons;
using UnityEngine;

namespace UI.Customization
{
    public class StartPanel : MonoBehaviour
    {
        [SerializeField] private UnitButton _startButton;

        private void Awake()
        {
            _startButton.Pressed
                .Subscribe(_ => StartFight())
                .AddTo(this);
        }

        private void StartFight()
        {
            Debug.Log("Запустить след сцену битвы");            
        }
    }
}