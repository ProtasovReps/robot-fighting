using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public abstract class ObservableButton<T> : MonoBehaviour
    {
        private readonly Subject<T> _pressed = new();

        [SerializeField] private Button _button;

        public Observable<T> Pressed => _pressed;
        
        protected abstract T Get();
        
        private void Awake()
        {
            T returnable = Get();
            
            _button.onClick.AddListener(() => _pressed.OnNext(returnable));
        }
    }
}