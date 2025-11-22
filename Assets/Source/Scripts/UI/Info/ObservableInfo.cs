using System;
using R3;
using TMPro;
using UnityEngine;

namespace UI.Info
{
    public abstract class ObservableInfo : MonoBehaviour
    {
        private const string OutputFormat = "0.#";
        
        [SerializeField] private TMP_Text _text;

        private Func<float> _getInfoFunc;
        
        protected void Initialize(Observable<Unit> observable, Func<float> getInfoAction)
        {
            _getInfoFunc = getInfoAction;
            
            observable
                .Subscribe(_ => ShowValue())
                .AddTo(this);
            
            ShowValue();
        }

        private void ShowValue()
        {
            _text.text = _getInfoFunc().ToString(OutputFormat);
        }
    }
}