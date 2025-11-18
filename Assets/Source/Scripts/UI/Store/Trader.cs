using Interface;
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
        private IMoneySpendable _moneySpendable;

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
        }

        public void Initialize(IMoneySpendable moneySpendable, BuyObservableButton[] buyButtons)
        {
            _buyButtons = buyButtons;
            _moneySpendable = moneySpendable;

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

            if (_moneySpendable.TrySpend(price) == false)
            {
                return;
            }

            YG2.saves.SellableViews.Add(sellable);
            goodPanel.SetEnable(false);
        }
    }
}