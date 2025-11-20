using System;
using Interface;
using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Panel;
using UnityEngine;
using YG;

namespace UI.Store
{
    public class Trader : MonoBehaviour
    {
        [SerializeField] private BuyObservableButton[] _buyButtons;
        
        private CompositeDisposable _subscriptions;
        private IMoneySpendable _moneySpendable;

        [Inject]
        private void Inject(IMoneySpendable moneySpendable)
        {
            _moneySpendable = moneySpendable;
        }

        private void Awake()
        {
            _subscriptions = new CompositeDisposable(_buyButtons.Length);

            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].Pressed
                    .Subscribe(Sell)
                    .AddTo(_subscriptions);
            }
        }

        private void OnDestroy()
        {
            _subscriptions?.Dispose();
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