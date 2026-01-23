using System.Collections.Generic;
using Extensions;
using UI.Store;

namespace YG.Saver
{
    public class ImplantSaver : EquipmentSaver
    {
        private readonly List<int> _implantViews;
        
        public ImplantSaver(Hasher hasher) 
            : base(hasher)
        {
            _implantViews = new List<int>(YG2.saves.Implants);
        }

        public override void Save()
        {
            YG2.saves.Implants = _implantViews;
        }
        
        public bool Contains(ImplantView implantView)
        {
            return _implantViews.Contains(GetHash(implantView.Name));
        }
        
        public void Add(ImplantView implantView)
        {
            _implantViews.Add(GetHash(implantView.Name));
        }
    }
}