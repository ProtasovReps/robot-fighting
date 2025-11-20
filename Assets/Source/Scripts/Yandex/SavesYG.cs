using System.Collections.Generic;
using CharacterSystem;
using Extensions;
using UI.Store;

namespace YG
{
    public partial class SavesYG
    {
        public int Money = 600;
        public float HealthStat = 100;
        public float SpeedStat = 5.5f;
        public float DamageStat = 3.5f;
        public float BlockStat = 20f;
        public float ImpulseForce = 0.5f;
        public int SceneIndex = 2;
        public bool IfBeatedGame = false;
        public List<SellableView> SellableViews = new();
        public Dictionary<AttackType, ImplantView> SettedImplants = new();
        public Fighter SettedFighter;
        public List<Fighter> Fighters = new();
    }
}