using System.Collections.Generic;
using Extensions;
using Interface;
using UI.Store;

namespace YG.Saver
{
    public class ImplantSaver : ISaver
    {
        private readonly List<string> _implantViews;
        private readonly Hasher<ImplantView> _hasher;
        
        public ImplantSaver(Hasher<ImplantView> hasher)
        {
            _implantViews = new List<string>(YG2.saves.Implants);
            _hasher = hasher;
        }

        public bool Contains(ImplantView implantView)
        {
            return _implantViews.Contains(GetHash(implantView));
        }
        
        public void Add(ImplantView implantView)
        {
            _implantViews.Add(GetHash(implantView));
        }
        
        public void Save()
        {
            YG2.saves.Implants = _implantViews;
        }

        private string GetHash(ImplantView implantView)
        {
            return _hasher.GetHash(implantView, implantView.Name);
        }
    }
}