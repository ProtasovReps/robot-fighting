using CharacterSystem;
using UI.Info;
using UnityEngine;

namespace UI.Store
{
    public class StoreInitializer : MonoBehaviour
    {
        [SerializeField] private Trader _trader;
        [SerializeField] private IntegerView _walletView;
        [SerializeField] private BuyGoodButton[] _buyGoodButtons;
        
        private void Awake()
        {
            Wallet wallet = new();
            
            _walletView.Initialize(wallet);
            _trader.Initialize(wallet, _buyGoodButtons);
        }
    }
}