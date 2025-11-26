using System.Collections.Generic;
using Extensions;
using Interface;
using UI.Store;

namespace YG.Saver
{
    public class EquipedImplantSaver : ISaver
    {
        private readonly Dictionary<AttackType, string> _implants;
        private readonly Hasher<ImplantView> _hasher;
        
        public EquipedImplantSaver(Hasher<ImplantView> hasher)
        {
            _implants = new Dictionary<AttackType, string>
            {
                { AttackType.UpAttack, YG2.saves.UpAttackImplant },
                { AttackType.DownAttack, YG2.saves.DownAttackImplant },
                { AttackType.Super, YG2.saves.SuperAttackImplant }
            };

            _hasher = hasher;
        }

        public void Save()
        {
            YG2.saves.UpAttackImplant = _implants[AttackType.UpAttack];
            YG2.saves.DownAttackImplant = _implants[AttackType.DownAttack];
            YG2.saves.SuperAttackImplant = _implants[AttackType.Super];
        }
     
        public void Set(AttackType attackType, ImplantView implantView)
        {
            ValidateDictionary(attackType);
            
            _implants[attackType] = GetHash(implantView);
        }

        public bool IsSetted(AttackType attackType, ImplantView implantView)
        {
            ValidateDictionary(attackType);

            return _implants[attackType] == GetHash(implantView);
        }

        private void ValidateDictionary(AttackType attackType)
        {
            if (_implants.ContainsKey(attackType) == false)
                throw new KeyNotFoundException(nameof(attackType));
        }

        private string GetHash(ImplantView implantView)
        {
            return _hasher.GetHash(implantView, implantView.Name);
        }
    }
}