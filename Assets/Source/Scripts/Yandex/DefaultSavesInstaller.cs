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
        
        public void Install()
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
    }
}