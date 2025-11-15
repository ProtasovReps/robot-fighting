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
        [SerializeField] private TMP_Text _effect;
        [SerializeField] private Image _goodImage;
        [SerializeField] private Image _rarenessImage;
        
        private SellableView _sellableView;

        public void Set(SellableView sellableView)
        {
            _name.text = sellableView.Name;
            _price.text = sellableView.Price.ToString();
            _effect.text = sellableView.Effect.ToString();
            _goodImage.sprite = sellableView.SellableImage;
            _rarenessImage.sprite = sellableView.Background;
            _sellableView = sellableView;
        }

        public SellableView Get()
        {
            return _sellableView;
        }
    }
}