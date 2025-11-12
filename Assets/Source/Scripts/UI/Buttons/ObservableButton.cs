using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public abstract class ObservableButton<T> : MonoBehaviour
    where T : ObservableButton<T>
    {
        private readonly Subject<T> _pressed = new();

        [SerializeField] private Button _button;

        public Observable<T> Pressed => _pressed;
        
        private void Awake()
        {
            _button.onClick.AddListener(() => _pressed.OnNext(this as T));
        }
    }
}