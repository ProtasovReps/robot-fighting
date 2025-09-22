using Interface;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace UI.Button
{
    public class ButtonEnableSwitcher<T> : MonoBehaviour
    where T : IFloatValueChangeable
    {
        private const float EnabledTransparancy = 1f;
        
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _disabledTransparency;

        private T _valueChangeable;

        [Inject]
        private void Inject(T valueChangeable)
        {
            _valueChangeable = valueChangeable;
        }
        
        private void Awake()
        {
            _valueChangeable.Value
                .Where(_ => IsFilled())
                .Subscribe(_ => Enable())
                .AddTo(this);
            
            _valueChangeable.Value
                .Where(_ => IsFilled() == false)
                .Subscribe(_ => Disable())
                .AddTo(this);
        }

        private void Enable()
        {
            _group.alpha = EnabledTransparancy;
        }

        private void Disable()
        {
            _group.alpha = _disabledTransparency;
        }

        private bool IsFilled()
        {
            return _valueChangeable.Value.CurrentValue >= _valueChangeable.MaxValue;
        }
    }
}