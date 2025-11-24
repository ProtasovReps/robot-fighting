using System.Collections.Generic;
using Extensions;
using Interface;
using UI.Store;

namespace YG.Saver
{
    public class PlayerImplantSave : ISaver
    {
        private readonly Dictionary<AttackType, ImplantView> _implants;

        public PlayerImplantSave()
        {
            _implants = new Dictionary<AttackType, ImplantView>
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
        
        public ImplantView Get(AttackType attackType)
        {
            ValidateDictionary(attackType);

            return _implants[attackType];
        }
        
        public void Set(AttackType attackType, ImplantView implantView)
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