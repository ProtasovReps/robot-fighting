using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace YG.Localization
{
    public class LanguageButton : MonoBehaviour
    {
        private const int OnePlus = 1;

        [SerializeField] private Sprite _ru;
        [SerializeField] private Sprite _en;
        [SerializeField] private Sprite _tr;
        [SerializeField] private Image _image;
        [SerializeField] private UnitButton _button;

        private string[] _languages;
        private Dictionary<string, Sprite> _sprites;
        private int _currentIndex;
        
        private void Awake()
        {
            _sprites = new Dictionary<string, Sprite>
            {
                { "ru", _ru },
                { "en", _en },
                { "tr", _tr }
            };

            _languages = _sprites.Select(element => element.Key).ToArray();
            _currentIndex = Array.IndexOf(_languages, YG2.lang);
            
            _button.Pressed
                .Subscribe(_ => SwitchLanguage())
                .AddTo(this);
        }

        private void SwitchLanguage()
        {
            _currentIndex = (_currentIndex + OnePlus) % _languages.Length;

            YG2.SwitchLanguage(_languages[_currentIndex]);
            _image.sprite = _sprites[YG2.lang];
        }
    }
}