using TMPro;
using UnityEngine;

namespace YG.Localization
{
    public class Translator : MonoBehaviour
    {
        private const string Russian = "ru";
        private const string Turkish = "tr";
        
        protected void SetLanguage(string language, TMP_Text text, string ru, string en, string tr)
        {
            string translation;

            if (language == Russian)
                translation = ru;
            else if (language == Turkish)
                translation = tr;
            else
                translation = en;

            text.text = translation;
        }
    }
}