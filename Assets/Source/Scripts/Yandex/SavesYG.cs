using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Money = 20000;
        public float HealthStat = 80f;
        public float SpeedStat = 5f;
        public float DamageStat = 15f;
        public float BlockStat = 20;
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