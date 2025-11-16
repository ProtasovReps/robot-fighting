using CharacterSystem;
using R3;
using TMPro;
using UI.Buttons;
using UI.Customization;
using UnityEngine;
using YG;

namespace UI.Panel
{
    public class BuySkinPanel : MonoBehaviour
    {
        private readonly Subject<Unit> _notEnoughMoney = new();
        
        [SerializeField] private UnitButton _buyButton;
        [SerializeField] private TMP_Text _price;

        private Wallet _wallet;
        private SkinView _sellectedSkin;

        public Observable<Unit> NotEnoughMoney => _notEnoughMoney;
        
        public void Initialize(FighterShowcase fighterShowcase, Wallet wallet)
        {
            _wallet = wallet;

            fighterShowcase.SkinChanged
                .Subscribe(SetSkin)
                .AddTo(this);

            _buyButton.Pressed
                .Subscribe(_ => BuySkin())
                .AddTo(this);
        }

        private void SetSkin(SkinView skinView)
        {
            if (YG2.saves.Fighters.Contains(skinView.Fighter))
            {
                _buyButton.gameObject.SetActive(false);
                return;
            }

            _sellectedSkin = skinView;
            _price.text = skinView.Price.ToString();
            _buyButton.gameObject.SetActive(true);
        }

        private void BuySkin()
        {
            if (_wallet.Value.CurrentValue < _sellectedSkin.Price)
            {
                _notEnoughMoney.OnNext(Unit.Default);
                return;
            }

            _wallet.Spend(_sellectedSkin.Price);
            YG2.saves.Fighters.Add(_sellectedSkin.Fighter);
            _buyButton.gameObject.SetActive(false);
        }
    }
}