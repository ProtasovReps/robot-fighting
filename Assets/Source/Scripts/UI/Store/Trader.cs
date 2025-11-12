using System.Collections.Generic;
using CharacterSystem;
using R3;
using UI.ButtonSwitchers;
using UnityEngine;
using YG;

namespace UI.Store
{
    public class Trader : MonoBehaviour
    {
        [SerializeField] private GoodsRandomizer _goodsRandomizer;
        [SerializeField] private ButtonSwitcher[] _buttonSwitchers;
        
        private BuyGoodButton[] _buyButtons;
        private CompositeDisposable _subscriptions;
        private Wallet _wallet;
        
        public void Initialize(Wallet wallet, BuyGoodButton[] buyGoodButtons)
        {
            _buyButtons = buyGoodButtons;
            _wallet = wallet;
            
            DistributeGoods();
        }
        
        private void DistributeGoods()
        {
            Unsubscribe();
            
            _subscriptions = new CompositeDisposable(_buyButtons.Length);
            
            Queue<GoodView> goods = _goodsRandomizer.Get();

            for (int i = 0; i < _buyButtons.Length; i++)
            {
                GoodView good = goods.Dequeue();
                
                _buyButtons[i].SetGood(good);
                _buttonSwitchers[i].Enable();

                _buyButtons[i].Pressed
                    .Subscribe(Sell)
                    .AddTo(_subscriptions);
            }
        }

        private void Sell(BuyGoodButton buyGoodButton)
        {
            GoodView good = buyGoodButton.Get();

            if (_wallet.Value.CurrentValue < good.Price)
                return;

            _wallet.Spend(good.Price);
            YG2.saves.Goods.Add(good.Good);
        }

        private void Unsubscribe()
        {
            _subscriptions?.Dispose();
        }
    }
}