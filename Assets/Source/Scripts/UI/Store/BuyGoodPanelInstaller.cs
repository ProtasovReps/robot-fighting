using System;
using System.Collections.Generic;
using UI.Panel;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace UI.Store
{
    public class BuyGoodPanelInstaller : MonoBehaviour
    {
        [SerializeField] private GoodPanel[] _buyGoodPanels;
        [SerializeField] private SellableView[] _goods;

        private List<SellableView> _playerSellables;
        
        public void Initialize()
        {
            if (_buyGoodPanels.Length > _goods.Length)
                throw new ArgumentOutOfRangeException(nameof(_buyGoodPanels));

            _playerSellables = YG2.saves.SellableViews;
        }

        public void Randomize()
        {
            SellableView tempSellable;

            for (int i = 0; i < _goods.Length; i++)
            {
                int randomIndex = Random.Range(0, _goods.Length);

                tempSellable = _goods[randomIndex];
                _goods[randomIndex] = _goods[i];
                _goods[i] = tempSellable;
            }
            
            for (int i = 0; i < _buyGoodPanels.Length; i++)
            {
                _buyGoodPanels[i].Set(_goods[i]);
                CheckPlayerSellables(_buyGoodPanels[i]);
            }
        }

        private void CheckPlayerSellables(GoodPanel goodPanel)
        {
            SellableView sellable = goodPanel.Get();
            bool ifNewItem = _playerSellables.Contains(sellable) == false;

            goodPanel.SetEnable(ifNewItem);
        }
    }
}