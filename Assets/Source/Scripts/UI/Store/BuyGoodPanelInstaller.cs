using System;
using Reflex.Attributes;
using UI.Panel;
using UnityEngine;
using YG.Saver;
using Random = UnityEngine.Random;

namespace UI.Store
{
    public class BuyGoodPanelInstaller : MonoBehaviour
    {
        [SerializeField] private GoodPanel[] _buyGoodPanels;
        [SerializeField] private SellableView[] _goods;

        private GoodSaver _goodSaver;

        [Inject]
        private void Inject(GoodSaver goodSaver)
        {
            if (_buyGoodPanels.Length > _goods.Length)
                throw new ArgumentOutOfRangeException(nameof(_buyGoodPanels));
            
            _goodSaver = goodSaver;
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
            bool ifNewItem = _goodSaver.Contains(sellable) == false;

            goodPanel.SetEnable(ifNewItem);
        }
    }
}