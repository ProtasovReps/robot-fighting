using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using Interface;

namespace YG.Saver
{
    public class PlayerImplantSave : ISaver
    {
        private readonly Dictionary<AttackType, AttackImplant> _implants;

        public PlayerImplantSave()
        {
            _implants = new Dictionary<AttackType, AttackImplant>
            {
                { AttackType.UpAttack, YG2.saves.UpAttackImplant },
                { AttackType.DownAttack, YG2.saves.DownAttackImplant },
                { AttackType.Super, YG2.saves.SuperAttackImplant }
            };
        }

        public void Save()
        {
            YG2.saves.UpAttackImplant = _implants[AttackType.UpAttack];
            YG2.saves.DownAttackImplant = _implants[AttackType.DownAttack];
            YG2.saves.SuperAttackImplant = _implants[AttackType.Super];
        }
        
        public AttackImplant Get(AttackType attackType)
        {
            ValidateDictionary(attackType);

            return _implants[attackType];
        }
        
        public void Set(AttackType attackType, AttackImplant implantView)
        {
            ValidateDictionary(attackType);

            _implants[attackType] = implantView;
        }

        private void ValidateDictionary(AttackType attackType)
        {
            if (_implants.ContainsKey(attackType) == false)
                throw new KeyNotFoundException(nameof(attackType));
        }
    }
}