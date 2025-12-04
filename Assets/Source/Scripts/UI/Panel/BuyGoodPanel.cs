using Reflex.Attributes;
using TMPro;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;
using YG.Localization;
using YG.Saver;

namespace UI.Panel
{
    public class BuyGoodPanel : SwitchablePanel
    {
        [SerializeField] private ImplantView _sellableView;
        [SerializeField] private ImplantTranslation _translation;
        [SerializeField] private Image _goodImage;
        [SerializeField] private Image _goodTypeImage;
        [SerializeField] private TMP_Text _price;

        private ImplantSaver _implantSaver;

        private void Start()
        {
            CheckPlayerSellables(_implantSaver);
        }

        [Inject]
        private void Inject(ImplantSaver implantSaver)
        {
            _implantSaver = implantSaver;
        }

        private void Awake()
        {
            _translation.Translate(_sellableView);
            _price.text = _sellableView.Price.ToString();
            _goodImage.sprite = _sellableView.ImplantImage;
            _goodTypeImage.sprite = _sellableView.ImplantTypeImage;
        }

        public ImplantView Get()
        {
            return _sellableView;
        }

        private void CheckPlayerSellables(ImplantSaver implantSaver)
        {
            bool isNewItem = implantSaver.Contains(_sellableView) == false;

            SetEnable(isNewItem);
        }
    }
}