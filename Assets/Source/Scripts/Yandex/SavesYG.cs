using System.Collections.Generic;
using CharacterSystem;
using UI.Store;

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
        public List<ImplantView> Implants = new();
        public ImplantView UpAttackImplant;
        public ImplantView DownAttackImplant;
        public ImplantView SuperAttackImplant;
        public List<Fighter> Fighters = new();
        public Fighter SettedFighter;
    }
}