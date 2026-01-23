using System;
using Extensions;
using Interface;

namespace YG.Saver
{
    public abstract class EquipmentSaver : ISaver
    {
        private readonly Hasher _hasher;
        
        protected EquipmentSaver(Hasher hasher)
        {
            if (hasher == null)
            {
                throw new ArgumentNullException(nameof(hasher));
            }
            
            _hasher = hasher;
        }
        
        public abstract void Save();

        protected int GetHash(string equipmentName)
        {
            return _hasher.GetHash(equipmentName);
        }
    }
}