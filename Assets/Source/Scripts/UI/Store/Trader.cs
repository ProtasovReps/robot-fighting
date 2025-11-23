using Interface;
using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Panel;
using UnityEngine;
using YG.Saver;

namespace UI.Store
{
    public class Trader : MonoBehaviour
    {
        [SerializeField] private BuyObservableButton[] _buyButtons;
        
        private CompositeDisposable _subscriptions;
        private IMoneySpendable _moneySpendable;
        private GoodSaver _goodSaver;
        
        [Inject]
        private void Inject(IMoneySpendable moneySpendable, GoodSaver goodSaver)
        {
            _moneySpendable = moneySpendable;
            _goodSaver = goodSaver;
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

        private void Sell(BuyGoodPanel buyGoodPanel)
        {
            SellableView sellable = buyGoodPanel.Get();
            int price = sellable.Price;

            if (_moneySpendable.TrySpend(price) == false)
            {
                return;
            }

            _goodSaver.Add(sellable);
            buyGoodPanel.SetEnable(false);
        }
    }
}