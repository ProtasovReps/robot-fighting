using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Guide.Pointers
{
    public class DisappearingPointer : MonoBehaviour
    {
        private readonly Subject<Unit> _disappeared = new();
        
        [SerializeField] private Button _button;

        public Observable<Unit> Disappeared => _disappeared;

        private void Awake()
        {
            _button.onClick.AddListener(Disappear);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Disappear);
        }

        private void Disappear()
        {
            gameObject.SetActive(false);
            _disappeared.OnNext(Unit.Default);
        }
    }
}