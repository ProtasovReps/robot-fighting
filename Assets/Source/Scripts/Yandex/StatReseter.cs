using R3;
using UI.Buttons;
using UnityEngine;

namespace YG
{
    public class StatReseter : MonoBehaviour
    {
        [SerializeField] private UnitButton _resetButton;
        [SerializeField] private DefaultSavesInstaller _savesInstaller;

        private void Awake()
        {
            _resetButton.Pressed
                .Subscribe(_ => _savesInstaller.InstallStats())
                .AddTo(this);
        }
    }
}