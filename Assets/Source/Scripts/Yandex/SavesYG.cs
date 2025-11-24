using System.Collections.Generic;
using CharacterSystem;
using Extensions;
using ImplantSystem.AttackImplants;
using UI.Customization;
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
        public List<SellableView> SellableViews;
        public AttackImplant UpAttackImplant;
        public AttackImplant DownAttackImplant;
        public AttackImplant SuperAttackImplant;
        public List<Fighter> Fighters;
        public Fighter SettedFighter;
    }
}