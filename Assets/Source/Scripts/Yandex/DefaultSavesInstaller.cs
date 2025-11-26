using CharacterSystem;
using Extensions;
using UI.Store;
using UnityEngine;
using YG.Saver;

namespace YG
{
    public class DefaultSavesInstaller : MonoBehaviour
    {
        [SerializeField] private ImplantView _defaultUpAttackImplant;
        [SerializeField] private ImplantView _defaultDownAttackImplant;
        [SerializeField] private ImplantView _defaultSuperAttackImplant;
        [SerializeField] private Fighter _defaultFighter;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _block;
        
        public void InstallSellables()
        {
            Hasher<ImplantView> hasher = new();
            ImplantSaver implantSaver = new(hasher);
            EquipedImplantSaver equipedImplantSaver = new(hasher);
            SkinSaver skinSaver = new(new Hasher<Fighter>());
            
            equipedImplantSaver.Set(AttackType.UpAttack, _defaultUpAttackImplant);
            equipedImplantSaver.Set(AttackType.DownAttack, _defaultDownAttackImplant);
            equipedImplantSaver.Set(AttackType.Super, _defaultSuperAttackImplant);
            
            implantSaver.Add(_defaultUpAttackImplant);
            implantSaver.Add(_defaultDownAttackImplant);
            implantSaver.Add(_defaultSuperAttackImplant);
            
            skinSaver.Add(_defaultFighter);
            skinSaver.Set(_defaultFighter);
            
            implantSaver.Save();
            equipedImplantSaver.Save();
            skinSaver.Save();
        }

        public void InstallStats()
        {
            YG2.saves.HealthStat = _health;
            YG2.saves.DamageStat = _damage;
            YG2.saves.SpeedStat = _speed;
            YG2.saves.BlockStat = _block;
        }
    }
}