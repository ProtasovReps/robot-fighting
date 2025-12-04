using Ami.BroAudio;
using Interface;
using R3;
using Reflex.Attributes;
using TMPro;
using UI.Buttons;
using UI.Customization;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.Panel
{
    public class BuySkinPanel : MonoBehaviour
    {
        [SerializeField] private UnitButton _buyButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private SoundID _skinBaughtSound;

        private IMoneySpendable _moneySpendable;
        private SkinView _sellectedSkin;
        private SkinSaver _skinSaver;

        [Inject]
        private void Inject(FighterShowcase fighterShowcase, IMoneySpendable moneySpendable, SkinSaver skinSaver)
        {
            _moneySpendable = moneySpendable;
            _skinSaver = skinSaver;

            fighterShowcase.SkinChanged
                .Subscribe(SetSkin)
                .AddTo(this);

            _buyButton.Pressed
                .Subscribe(_ => BuySkin())
                .AddTo(this);
        }

        private void SetSkin(SkinView skinView)
        {
            if (_skinSaver.Contains(skinView.Fighter))
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

            YG2.MetricaSend(_sellectedSkin.name);
            BroAudio.Play(_skinBaughtSound);
            
            _skinSaver.Add(_sellectedSkin.Fighter);
            _buyButton.gameObject.SetActive(false);
        }
    }
}