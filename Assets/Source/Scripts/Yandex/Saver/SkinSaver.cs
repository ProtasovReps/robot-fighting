using System;
using System.Collections.Generic;
using CharacterSystem;
using Extensions;
using Interface;

namespace YG.Saver
{
    public class SkinSaver : ISaver
    {
        private readonly List<string> _fighters;
        private readonly Hasher<Fighter> _hasher;
        
        private string _settedFighter;

        public SkinSaver(Hasher<Fighter> hasher)
        {
            _fighters = new List<string>(YG2.saves.Fighters);
            _hasher = hasher;
            _settedFighter = YG2.saves.SettedFighter;
        }

        public void Add(Fighter fighter)
        {
            string hash = GetHash(fighter);
            
            if (_fighters.Contains(hash))
                throw new ArgumentException(nameof(fighter));

            _fighters.Add(hash);
        }

        public bool Contains(Fighter fighter)
        {
            return _fighters.Contains(GetHash(fighter));
        }

        public bool IsSetted(Fighter fighter)
        {
            return _settedFighter == GetHash(fighter);
        }
        
        public void Set(Fighter fighter)
        {
            _settedFighter = GetHash(fighter);
        }

        public void Save()
        {
            YG2.saves.SettedFighter = _settedFighter;
            YG2.saves.Fighters = _fighters;
        }

        private string GetHash(Fighter fighter)
        {
            return _hasher.GetHash(fighter, fighter.name);
        }
    }
}