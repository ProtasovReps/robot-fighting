using System.Collections.Generic;
using CharacterSystem;
using UI.Store;

namespace YG
{
    public partial class SavesYG
    {
        public int Money = 600;
        public float HealthStat = 1000;
        public float SpeedStat = 6f;
        public float DamageStat = 100;
        public float BlockStat = 60;
        public int SceneIndex = 3;
        public bool IfBeatedGame = false;
        public List<ImplantView> Implants = new();
        public ImplantView UpAttackImplant;
        public ImplantView DownAttackImplant;
        public ImplantView SuperAttackImplant;
        public List<Fighter> Fighters = new();
        public Fighter SettedFighter;
    }
}