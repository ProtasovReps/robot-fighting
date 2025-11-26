using Interface;
using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Panel;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.Store
{
    public class Trader : MonoBehaviour
    {
        [SerializeField] private BuyObservableButton[] _buyButtons;
        
        private CompositeDisposable _subscriptions;
        private IMoneySpendable _moneySpendable;
        private ImplantSaver _implantSaver;
        private ProgressSaver _progressSaver;
        
        [Inject]
        private void Inject(IMoneySpendable moneySpendable, ImplantSaver implantSaver, ProgressSaver progressSaver)
        {
            _moneySpendable = moneySpendable;
            _implantSaver = implantSaver;
            _progressSaver = progressSaver;
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
            ImplantView implantView = buyGoodPanel.Get();
            int price = implantView.Price;

            if (_moneySpendable.TrySpend(price) == false)
            {
                return;
            }

            _implantSaver.Add(implantView);
            _progressSaver.Save();
            buyGoodPanel.SetEnable(false);
        }
    }
}