using System.Collections.Generic;
using CharacterSystem;
using UI.Buttons;
using UI.Info;
using UI.Panel;
using UI.Store;
using UI.Switchers;
using UnityEngine;
using YG;
using YG.Saver;

namespace UI.Customization
{
    public class CustomizationInitializer : MonoBehaviour
    {
        [SerializeField] private EquipmentBag _equipmentBag;
        [SerializeField] private EquipedImplant[] _playerEquipments;
        [SerializeField] private IntegerView _walletView;
        [SerializeField] private EquipmentPanelSwitcher _panelSwitcher;
        [SerializeField] private FighterSkinCustomization _skinCustomization;
        [SerializeField] private MoneyEffectInitializer _effectInitializer;
        
        private void Awake()
        {
            Wallet wallet = new(YG2.saves.Money);
            WalletSaver saver = new(wallet);
            
            _equipmentBag.Initialize();
            _walletView.Initialize(wallet);
            _skinCustomization.Initialize(wallet);
            _effectInitializer.Initialize(wallet);
            
            List<EquipButton> equipButtons = new();

            foreach (EquipmentPanel panel in _equipmentBag.EquipmentPanels)
            {
                EquipButton button = panel.GetComponent<EquipButton>();
                equipButtons.Add(button);
            }
            
            _panelSwitcher.Initialize(_equipmentBag.EquipmentPanels, equipButtons);
            
            for (int i = 0; i < _playerEquipments.Length; i++)
            {
                _playerEquipments[i].Initialize(equipButtons);
            }
        }
    }
}