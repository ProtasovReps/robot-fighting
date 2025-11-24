using System.Collections.Generic;
using Interface;
using UI.Store;

namespace YG.Saver
{
    public class GoodSaver : ISaver
    {
        private readonly List<ImplantView> _implantViews;

        public GoodSaver()
        {
            _implantViews = new(YG2.saves.Implants);
        }

        public IEnumerable<ImplantView> ImplantViews => _implantViews;
        
        public bool Contains(ImplantView implantView)
        {
            return _implantViews.Contains(implantView);
        }
        
        public void Add(ImplantView implantView)
        {
            _implantViews.Add(implantView);
        }
        
        public void Save()
        {
            YG2.saves.Implants = _implantViews;
        }
    }
}