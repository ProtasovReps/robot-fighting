using System;
using System.Collections.Generic;
using CharacterSystem;
using Extensions;

namespace YG.Saver
{
    public class SkinSaver : EquipmentSaver
    {
        private readonly List<int> _fighters;
        
        private int _settedFighter;

        public SkinSaver(Hasher hasher) 
            : base(hasher)
        {
            _fighters = new List<int>(YG2.saves.Fighters);
            _settedFighter = YG2.saves.SettedFighter;
        }

        public override void Save()
        {
            YG2.saves.SettedFighter = _settedFighter;
            YG2.saves.Fighters = _fighters;
        }

        public void Add(Fighter fighter)
        {
            int hash = GetHash(fighter.name);

            if (_fighters.Contains(hash))
            {
                throw new ArgumentException(nameof(fighter));
            }

            _fighters.Add(hash);
        }

        public bool Contains(Fighter fighter)
        {
            return _fighters.Contains(GetHash(fighter.name));
        }

        public bool IsSetted(Fighter fighter)
        {
            return _settedFighter == GetHash(fighter.name);
        }
        
        public void Set(Fighter fighter)
        {
            _settedFighter = GetHash(fighter.name);
        }
    }
}