using Interface;
using R3;
using TMPro;
using UnityEngine;

namespace HealthSystem
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Initialize(IValueChangeable<float> valueChangeable)
        {
            valueChangeable.Value
                .Subscribe(UpdateValue)
                .AddTo(this);
        }

        private void UpdateValue(float newValue) // через UniTask можно анимировать красиво 0o0
        {
            _text.text = newValue.ToString();
        }
    }
}