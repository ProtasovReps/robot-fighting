using CharacterSystem;
using Interface;

namespace YG.Saver
{
    public class WalletSaver : ISaver
    {
        private readonly Wallet _wallet;
        
        public WalletSaver(Wallet wallet)
        {
            _wallet = wallet;
        }
        
        public void Save()
        {
            YG2.saves.Money = _wallet.Value.CurrentValue;
        }
    }
}