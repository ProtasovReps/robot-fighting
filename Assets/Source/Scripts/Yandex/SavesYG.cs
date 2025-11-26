using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int Money = 400;
        public float HealthStat = 80;
        public float SpeedStat = 5f;
        public float DamageStat = 15;
        public float BlockStat = 20;
        public int SceneIndex = 3; 
        public bool IsBeatedGame = false;
        public bool IsGuidePassed = false;
        public List<string> Implants = new();
        public List<string> Fighters = new();
        public string SettedFighter;
        public string UpAttackImplant;
        public string DownAttackImplant;
        public string SuperAttackImplant;
    }
}