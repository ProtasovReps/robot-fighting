using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        public int GloryPoints = 0;
        public int Money = 400;
        public float HealthStat = 80f;
        public float SpeedStat = 7f;
        public float DamageStat = 15f;
        public float BlockStat = 20;
        public int SceneIndex = 3;
        public float SoundVolume = 0.035f;
        public bool IsGuidePassed = false;
        public List<int> Implants = new ();
        public List<int> Fighters = new ();
        public int SettedFighter;
        public int UpAttackImplant;
        public int DownAttackImplant;
        public int SuperAttackImplant;
    }
}