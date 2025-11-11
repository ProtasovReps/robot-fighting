using TMPro;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class BuyGoodButton : ObservableButton<BuyGoodButton>
    {
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _goodImage;
        [SerializeField] private Image _rarenessImage;
        
        private GoodView _good;

        public void SetGood(GoodView goodView)
        {
            _good = goodView;
            _price.text = _good.Price.ToString();
            _goodImage.sprite = _good.GoodImage;
            _rarenessImage.sprite = _good.RarenessImage;
        }

        public GoodView Get()
        {
            return _good;
        }
        
        protected override BuyGoodButton GetButton()
        {
            return this;
        }
    }
}