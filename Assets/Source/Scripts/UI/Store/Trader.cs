using System.Collections.Generic;
using CharacterSystem;
using R3;
using UnityEngine;
using YG;

namespace UI.Store
{
    public class Trader : MonoBehaviour
    {
        private readonly Subject<Unit> _sold = new();

        [SerializeField] private GoodsRandomizer _goodsRandomizer;
        
        private BuyGoodButton[] _buyButtons;
        private CompositeDisposable _subscriptions;
        private Wallet _wallet;
        
        public Observable<Unit> Sold => _sold;

        public void Initialize(Wallet wallet, BuyGoodButton[] buyGoodButtons)
        {
            _buyButtons = buyGoodButtons;
            _wallet = wallet;
            
            _subscriptions = new CompositeDisposable(_buyButtons.Length);
            
            DistributeGoods();
        }
        
        private void DistributeGoods()
        {
            Unsubscribe();
            Queue<GoodView> goods = _goodsRandomizer.Get();

            for (int i = 0; i < _buyButtons.Length; i++)
            {
                GoodView good = goods.Dequeue();
                _buyButtons[i].SetGood(good);

                _buyButtons[i].Pressed
                    .Subscribe(Sell)
                    .AddTo(_subscriptions);
            }
        }

        private void Sell(BuyGoodButton buyGoodButton)
        {
            GoodView good = buyGoodButton.Get();

            if (YG2.saves.Money < good.Price)
                return;

            YG2.saves.Money -= good.Price;
            YG2.saves.Goods.Add(good.Good);
            _sold.OnNext(Unit.Default);
        }

        private void Unsubscribe()
        {
            _subscriptions.Dispose();
        }
    }
}