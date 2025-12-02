using TMPro;
using UnityEngine;

namespace YG.Localization
{
    public class TextTranslation : MonoBehaviour
    {
        private const string Russian = "ru";
        private const string Turkish = "tr";

        [TextArea(1, 5)] [SerializeField] private string _ru;
        [TextArea(1, 5)] [SerializeField] private string _en;
        [TextArea(1, 5)] [SerializeField] private string _tr;
        [SerializeField] private TMP_Text _textField;

        private void Awake()
        {
            SetLanguage(YG2.lang);
        }

        private void OnEnable()
        {
            YG2.onSwitchLang += SetLanguage;
        }

        private void OnDisable()
        {
            YG2.onSwitchLang -= SetLanguage;
        }

        private void SetLanguage(string language)
        {
            string translation;

            if (language == Russian)
                translation = _ru;
            else if (language == Turkish)
                translation = _tr;
            else
                translation = _en;

            _textField.text = translation;
        }
    }
}