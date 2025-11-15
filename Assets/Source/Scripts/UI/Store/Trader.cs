using CharacterSystem;
using R3;
using UI.Buttons;
using UI.Panel;
using UnityEngine;
using YG;

namespace UI.Store
{
    public class Trader : MonoBehaviour
    {
        private BuyObservableButton[] _buyButtons;
        private CompositeDisposable _subscriptions;
        private Wallet _wallet;

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
        }

        public void Initialize(Wallet wallet, BuyObservableButton[] buyButtons)
        {
            _buyButtons = buyButtons;
            _wallet = wallet;
            
            _subscriptions = new CompositeDisposable(_buyButtons.Length);

            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].Pressed
                    .Subscribe(Sell)
                    .AddTo(_subscriptions);
            }
        }

        private void Sell(GoodPanel goodPanel)
        {
            SellableView sellable = goodPanel.Get();
            int price = sellable.Price;
            
            if (_wallet.Value.CurrentValue < price)
                return;

            _wallet.Spend(price);
            YG2.saves.Goods.Add(sellable);
            goodPanel.SetEnable(false);
        }
    }
}