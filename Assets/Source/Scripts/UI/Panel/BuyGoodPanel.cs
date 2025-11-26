using Reflex.Attributes;
using TMPro;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;
using YG.Saver;

namespace UI.Panel
{
    public class BuyGoodPanel : SwitchablePanel
    {
        [SerializeField] private ImplantView _sellableView;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _goodImage;

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
            _name.text = _sellableView.Name;
            _price.text = _sellableView.Price.ToString();
            _goodImage.sprite = _sellableView.ImplantImage;
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