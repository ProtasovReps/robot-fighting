using CharacterSystem;
using Extensions;
using Interface;
using Reflex.Core;
using UI.Info;
using UI.Store;
using UnityEngine;
using YG;
using YG.Saver;

namespace Reflex
{
    public class TraderInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ProgressSaver _progressSaver;
        [SerializeField] private IntegerView _moneyView;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            Wallet wallet = new (YG2.saves.Money);
            WalletSaver walletSaver = new (wallet);
            ImplantSaver implantSaver = new (new Hasher<ImplantView>());
            
            _progressSaver.Add(walletSaver);
            _progressSaver.Add(implantSaver);
            _moneyView.Initialize(wallet);

            containerBuilder.AddSingleton(wallet, typeof(IMoneyAddable), typeof(IMoneySpendable));
            containerBuilder.AddSingleton(_progressSaver);
            containerBuilder.AddSingleton(implantSaver);
        }
    }
}