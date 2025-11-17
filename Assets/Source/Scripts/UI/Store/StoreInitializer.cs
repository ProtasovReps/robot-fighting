using CharacterSystem;
using UI.Buttons;
using UI.Info;
using UnityEngine;

namespace UI.Store
{
    public class StoreInitializer : MonoBehaviour
    {
        [SerializeField] private Trader _trader;
        [SerializeField] private IntegerView _walletView;
        [SerializeField] private BuyObservableButton[] _buyButtons;
        [SerializeField] private NotEnoughMoneyEffect _effect;
        
        private void Awake()
        {
            Wallet wallet = new();
            
            _walletView.Initialize(wallet);
            _trader.Initialize(wallet, _buyButtons);
            _effect.Initialize(wallet);
        }
    }
}