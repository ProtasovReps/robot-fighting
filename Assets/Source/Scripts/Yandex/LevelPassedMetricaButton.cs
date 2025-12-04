using R3;
using UI.Buttons;
using UnityEngine;

namespace YG
{
    public class LevelPassedMetricaButton : MonoBehaviour
    {
        [SerializeField] private UnitButton _unitButton;
        [SerializeField, Min(1)] private int _levelNumber;
        
        private void Awake()
        {
            _unitButton.Pressed
                .Subscribe(_ => YG2.MetricaSend($"{_levelNumber}Passed"))
                .AddTo(this);
        }
    }
}