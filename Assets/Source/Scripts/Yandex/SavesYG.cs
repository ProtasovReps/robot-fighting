using System.Collections.Generic;
using UI.Store;

namespace YG
{
    public partial class SavesYG
    {
        public int Money = 100;
        public float HealthStat = 100;
        public float SpeedStat = 5.5f;
        public float DamageStat = 3.5f;
        public float BlockStat = 20f;
        public float ImpulseForce = 0.5f;
        public List<Good> Goods = new();
    }
}