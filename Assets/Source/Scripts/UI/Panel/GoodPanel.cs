using TMPro;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class GoodPanel : SwitchablePanel
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _goodImage;
        
        private SellableView _sellableView;

        public void Set(SellableView sellableView)
        {
            _name.text = sellableView.Name;
            _price.text = sellableView.Price.ToString();
            _goodImage.sprite = sellableView.SellableImage;
            _sellableView = sellableView;
        }

        public SellableView Get()
        {
            return _sellableView;
        }
    }
}