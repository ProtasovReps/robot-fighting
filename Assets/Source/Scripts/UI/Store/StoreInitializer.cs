using CharacterSystem;
using UI.Buttons;
using UI.Info;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.Store
{
    public class StoreInitializer : MonoBehaviour
    {
        [SerializeField] private Trader _trader;
        [SerializeField] private IntegerView _walletView;
        [SerializeField] private BuyObservableButton[] _buyButtons;
        [SerializeField] private MoneyEffectInitializer _effectInitializer;
        
        private void Awake()
        {
            Wallet wallet = new(YG2.saves.Money);
            WalletSaver walletSaver = new(wallet);
            
            _walletView.Initialize(wallet);
            _trader.Initialize(wallet, _buyButtons);
            _effectInitializer.Initialize(wallet);
        }
    }
}