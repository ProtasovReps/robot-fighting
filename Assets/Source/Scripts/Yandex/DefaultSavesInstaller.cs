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
        
        public void Install(GoodSaver goodSaver, PlayerImplantSave implantSave, SkinSaver skinSaver)
        {
            implantSave.Set(AttackType.UpAttack, _defaultUpAttackImplant.AttackImplant);
            implantSave.Set(AttackType.DownAttack, _defaultDownAttackImplant.AttackImplant);
            implantSave.Set(AttackType.Super, _defaultSuperAttackImplant.AttackImplant);
            
            goodSaver.Add(_defaultUpAttackImplant);
            goodSaver.Add(_defaultDownAttackImplant);
            goodSaver.Add(_defaultSuperAttackImplant);
            
            skinSaver.Add(_defaultFighter);
            skinSaver.Set(_defaultFighter);
            
            goodSaver.Save();
            implantSave.Save();
            skinSaver.Save();
        }
    }
}