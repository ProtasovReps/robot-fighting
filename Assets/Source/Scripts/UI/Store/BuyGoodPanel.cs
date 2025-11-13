using Interface;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class BuyGoodPanel : MonoBehaviour
    {
        private readonly Subject<bool> _isEnableSwitched = new();
        
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _effect;
        [SerializeField] private Image _goodImage;
        [SerializeField] private Image _rarenessImage;
        
        private SellableView _sellableView;

        public Observable<bool> IsEnableSwitched => _isEnableSwitched;
        
        public void SetGood(SellableView sellableView)
        {
            _name.text = sellableView.Name;
            _price.text = sellableView.Price.ToString();
            _effect.text = sellableView.Effect.ToString();
            _goodImage.sprite = sellableView.SellableImage;
            _rarenessImage.sprite = sellableView.Background;
            _sellableView = sellableView;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnableSwitched.OnNext(isEnabled);
        }
        
        public SellableView Get()
        {
            return _sellableView;
        }
    }
}