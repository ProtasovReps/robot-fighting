using System;
using UI.Panel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Store
{
    public class BuyGoodPanelInitializer : MonoBehaviour
    {
        [SerializeField] private GoodPanel[] _buyGoodPanels;
        [SerializeField] private SellableView[] _goods;

        public void Initialize()
        {
            if (_buyGoodPanels.Length > _goods.Length)
                throw new ArgumentOutOfRangeException(nameof(_buyGoodPanels));
            
            Randomize();
            
            for (int i = 0; i < _buyGoodPanels.Length; i++)
            {
                _buyGoodPanels[i].Set(_goods[i]);
                _buyGoodPanels[i].SetEnable(true);
            }
        }

        private void Randomize()
        {
            SellableView tempSellable;
            
            for (int i = 0; i < _goods.Length; i++)
            {
                int randomIndex = Random.Range(0, _goods.Length);

                tempSellable = _goods[randomIndex];
                _goods[randomIndex] = _goods[i];
                _goods[i] = tempSellable;
            }
        }
    }
}