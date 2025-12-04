using TMPro;
using UnityEngine;

namespace YG.Localization
{
    public class AutoTranslation : Translator
    {
        [TextArea(1, 5)] [SerializeField] private string _ru;
        [TextArea(1, 5)] [SerializeField] private string _en;
        [TextArea(1, 5)] [SerializeField] private string _tr;
        [SerializeField] private TMP_Text _textField;

        private void Awake()
        {
            SetLanguage(YG2.lang, _textField, _ru, _en, _tr);
        }

        private void OnEnable()
        {
            YG2.onSwitchLang += language =>  SetLanguage(language, _textField, _ru, _en, _tr);
        }

        private void OnDisable()
        {
            YG2.onSwitchLang -= language =>  SetLanguage(language, _textField, _ru, _en, _tr);
        }
    }
}