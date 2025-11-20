using System.Collections.Generic;
using Extensions;
using Interface;
using UI.Store;

namespace YG.Saver
{
    public class ImplantSaver : ISaver
    {
        private readonly Dictionary<AttackType, ImplantView> _implants;

        public ImplantSaver()
        {
            _implants = new(YG2.saves.SettedImplants);
        }

        public void SetImplant(AttackType attackType, ImplantView implantView)
        {
            // if (_implants.ContainsKey(attackType) == false)
            //     throw new KeyNotFoundException(nameof(attackType)); // временно закоментировано

            _implants[attackType] = implantView;
        }
        
        public void Save()
        {
            YG2.saves.SettedImplants = _implants;
        }
    }
}