using System;
using System.Collections.Generic;
using CharacterSystem;
using Interface;
using UI.Customization;

namespace YG.Saver
{
    public class SkinSaver : ISaver
    {
        private readonly List<SkinView> _fighters;

        private SkinView _settedFighter;

        public SkinSaver()
        {
            _fighters = new(YG2.saves.Fighters);
            _settedFighter = YG2.saves.SettedFighter;
        }

        public void Add(SkinView fighter)
        {
            if (_fighters.Contains(fighter))
                throw new ArgumentException(nameof(fighter));

            _fighters.Add(fighter);
        }

        public bool Contains(SkinView fighter)
        {
            return _fighters.Contains(fighter);
        }

        public bool IsSetted(SkinView fighter)
        {
            return _settedFighter == fighter;
        }
        
        public void Set(SkinView fighter)
        {
            _settedFighter = fighter;
        }

        public void Save()
        {
            YG2.saves.SettedFighter = _settedFighter;
            YG2.saves.SettedFighter = _settedFighter;
        }
    }
}