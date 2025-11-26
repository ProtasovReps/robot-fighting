using System.Collections.Generic;
using CharacterSystem;
using Extensions;
using Interface;
using Reflex.Core;
using UI.Buttons;
using UI.Customization;
using UI.Info;
using UI.Panel;
using UI.Store;
using UI.Switchers;
using UnityEngine;
using YG;
using YG.Saver;

namespace Reflex
{
    public class CustomizationInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private ProgressSaver _progressSaver;
        [SerializeField] private IntegerView _moneyView;
        [SerializeField] private FighterShowcase _fighterShowcase;
        [SerializeField] private EquipmentBag _bag;
        [SerializeField] private EquipmentPanelSwitcher _panelSwitcher;
        [SerializeField] private EquipedImplant[] _playerEquipments;

        private Hasher<ImplantView> _implantHasher;
        
        private void Awake()
        {
            _bag.Initialize(new ImplantSaver(_implantHasher));
            InstallButtons();
            _fighterShowcase.Initialize();
        }

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            _implantHasher = new Hasher<ImplantView>();
            
            Wallet wallet = new(YG2.saves.Money);
            WalletSaver walletSaver = new(wallet);
            EquipedImplantSaver equipedImplantSaver = new(_implantHasher);
            SkinSaver skinSaver = new(new Hasher<Fighter>());
            
            _progressSaver.Add(walletSaver);
            _progressSaver.Add(skinSaver);
            _progressSaver.Add(equipedImplantSaver);
            _moneyView.Initialize(wallet);
            
            containerBuilder.AddSingleton(wallet, typeof(IMoneyAddable), typeof(IMoneySpendable));
            containerBuilder.AddSingleton(equipedImplantSaver);
            containerBuilder.AddSingleton(skinSaver);
            containerBuilder.AddSingleton(_progressSaver);
            containerBuilder.AddSingleton(_fighterShowcase);
        }

        private void InstallButtons()
        {
            List<EquipButton> equipButtons = new();

            foreach (EquipmentPanel panel in _bag.EquipmentPanels)
            {
                EquipButton button = panel.GetComponent<EquipButton>();
                equipButtons.Add(button);
            }
            
            _panelSwitcher.Initialize(_bag.EquipmentPanels, equipButtons);
            
            for (int i = 0; i < _playerEquipments.Length; i++)
            {
                _playerEquipments[i].Initialize(equipButtons);
            }
        }
    }
}