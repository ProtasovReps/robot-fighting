using AudioSystem.EventDependent;
using CharacterSystem;
using UI.Info;
using UnityEngine;

namespace UI.Store
{
    public class MoneyEffectInitializer : MonoBehaviour
    {
        [SerializeField] private NotEnoughMoneyEffect _effect;
        [SerializeField] private NotEnoughMoneySound _notEnoughMoneySound;
        [SerializeField] private MoneySpentSound _moneySpentSound;

        public void Initialize(Wallet wallet)
        {
            _effect.Initialize(wallet);
            _notEnoughMoneySound.Initialize(wallet);
            _moneySpentSound.Initialize(wallet);
        }
    }
}