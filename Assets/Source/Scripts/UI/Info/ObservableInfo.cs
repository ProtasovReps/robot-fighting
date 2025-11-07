using R3;
using TMPro;
using UnityEngine;

namespace UI.Info
{
    public abstract class ObservableInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        protected abstract float GetInfo();
        
        protected void Subscribe(Observable<Unit> observable)
        {
            observable
                .Subscribe(_ => ShowValue())
                .AddTo(this);
            
            ShowValue();
        }
        
        private void ShowValue()
        {
            _text.text = GetInfo().ToString();
        }
    }
}