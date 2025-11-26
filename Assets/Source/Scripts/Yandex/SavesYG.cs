using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Money = 400;
        public float HealthStat;
        public float SpeedStat;
        public float DamageStat;
        public float BlockStat;
        public int SceneIndex = 3;
        public float SoundVolume = 0.035f;
        public bool IsGuidePassed = false;
        public List<string> Implants = new();
        public List<string> Fighters = new();
        public string SettedFighter;
        public string UpAttackImplant;
        public string DownAttackImplant;
        public string SuperAttackImplant;
    }
}