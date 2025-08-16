using R3;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace HealthSystem
{
    public class HealthView<T> : MonoBehaviour
        where T : Health
    {
        [SerializeField] private TMP_Text _text;

        [Inject]
        private void Inject(T health)
        {
            health.Value
                .Subscribe(UpdateValue)
                .AddTo(this);
        }

        private void UpdateValue(float newValue) // через UniTask можно анимировать красиво 0o0
        {
            _text.text = newValue.ToString();
        }
    }
}