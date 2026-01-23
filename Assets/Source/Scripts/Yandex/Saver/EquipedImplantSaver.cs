using System.Collections.Generic;
using Extensions;
using UI.Store;

namespace YG.Saver
{
    public class EquipedImplantSaver : EquipmentSaver
    {
        private readonly Dictionary<AttackType, int> _implants;
        
        public EquipedImplantSaver(Hasher hasher)
            : base(hasher)
        {
            _implants = new Dictionary<AttackType, int>
            {
                { AttackType.UpAttack, YG2.saves.UpAttackImplant },
                { AttackType.DownAttack, YG2.saves.DownAttackImplant },
                { AttackType.Super, YG2.saves.SuperAttackImplant },
            };
        }

        public override void Save()
        {
            YG2.saves.UpAttackImplant = _implants[AttackType.UpAttack];
            YG2.saves.DownAttackImplant = _implants[AttackType.DownAttack];
            YG2.saves.SuperAttackImplant = _implants[AttackType.Super];
        }
     
        public void Set(AttackType attackType, ImplantView implantView)
        {
            ValidateDictionary(attackType);
            
            _implants[attackType] = GetHash(implantView.Name);
        }

        public bool IsSetted(AttackType attackType, ImplantView implantView)
        {
            ValidateDictionary(attackType);

            return _implants[attackType] == GetHash(implantView.Name);
        }

        private void ValidateDictionary(AttackType attackType)
        {
            if (_implants.ContainsKey(attackType) == false)
            {
                throw new KeyNotFoundException(nameof(attackType));
            }
        }
    }
}