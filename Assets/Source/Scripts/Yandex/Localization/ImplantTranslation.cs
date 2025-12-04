using TMPro;
using UI.Store;
using UnityEngine;

namespace YG.Localization
{
    public class ImplantTranslation : Translator
    {
        [SerializeField] private TMP_Text _text;

        public void Translate(ImplantView view)
        {
            SetLanguage(YG2.lang, _text, view.RuName, view.Name, view.TrName);
        }
    }
}