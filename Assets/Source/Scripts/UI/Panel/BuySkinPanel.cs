using Ami.BroAudio;
using Interface;
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
        [SerializeField] private UnitButton _buyButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private SoundID _skinBaughtSound;
        
        private IMoneySpendable _moneySpendable;
        private SkinView _sellectedSkin;

        public void Initialize(FighterShowcase fighterShowcase, IMoneySpendable moneySpendable)
        {
            _moneySpendable = moneySpendable;

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
            if (_moneySpendable.TrySpend(_sellectedSkin.Price) == false)
            {
                return;
            }

            BroAudio.Play(_skinBaughtSound);
            YG2.saves.Fighters.Add(_sellectedSkin.Fighter);
            _buyButton.gameObject.SetActive(false);
        }
    }
}