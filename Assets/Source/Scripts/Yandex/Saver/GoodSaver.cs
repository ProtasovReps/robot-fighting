using System.Collections.Generic;
using Interface;
using UI.Store;

namespace YG.Saver
{
    public class GoodSaver : ISaver
    {
        private readonly List<SellableView> _sellableViews;

        public GoodSaver()
        {
            _sellableViews = new(YG2.saves.SellableViews);
        }

        public bool Contains(SellableView sellableView)
        {
            return _sellableViews.Contains(sellableView);
        }
        
        public void Add(SellableView sellableView)
        {
            _sellableViews.Add(sellableView);
        }
        
        public void Save()
        {
            YG2.saves.SellableViews = _sellableViews;
        }
    }
}