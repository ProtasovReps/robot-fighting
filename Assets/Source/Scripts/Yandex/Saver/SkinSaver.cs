using System;
using System.Collections.Generic;
using CharacterSystem;
using Interface;

namespace YG.Saver
{
    public class SkinSaver : ISaver
    {
        private readonly List<Fighter> _fighters;

        private Fighter _settedFighter;

        public SkinSaver()
        {
            _fighters = new(YG2.saves.Fighters);
            _settedFighter = YG2.saves.SettedFighter;
        }

        public void Add(Fighter fighter)
        {
            if (_fighters.Contains(fighter))
                throw new ArgumentException(nameof(fighter));

            _fighters.Add(fighter);
        }

        public bool Contains(Fighter fighter)
        {
            return _fighters.Contains(fighter);
        }

        public bool IsSetted(Fighter fighter)
        {
            return _settedFighter == fighter;
        }
        
        public void Set(Fighter fighter)
        {
            _settedFighter = fighter;
        }

        public void Save()
        {
            YG2.saves.SettedFighter = _settedFighter;
            YG2.saves.Fighters = _fighters;
        }
    }
}