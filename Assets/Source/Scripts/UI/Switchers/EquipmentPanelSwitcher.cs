using System;
using System.Collections.Generic;
using Extensions;
using R3;
using UI.Buttons;
using UI.Panel;
using UI.Store;
using UnityEngine;

namespace UI.Switchers
{
    public class EquipmentPanelSwitcher : MonoBehaviour // потом подумать над монобехами, мб некоторые убрать можно
    {
        [SerializeField] private ImplantView _testImplant1; // mock
        [SerializeField] private ImplantView _testImplant2; // mock
        [SerializeField] private ImplantView _testImplatn3;
         
        private readonly Dictionary<AttackType, EquipmentPanel> _equipment = new();
        
        private Dictionary<AttackType, ImplantView> _mockImplants;// mock
        
        public void Initialize(IEnumerable<EquipmentPanel> equipmentPanels, IEnumerable<EquipButton> buttons)
        {
            _mockImplants = new Dictionary<AttackType, ImplantView> 
            {
                { _testImplant1.AttackImplant.Parameters.RequiredState, _testImplant1},
                { _testImplant2.AttackImplant.Parameters.RequiredState, _testImplant2},
                { _testImplatn3.AttackImplant.Parameters.RequiredState, _testImplatn3}
            };
                
            List<EquipmentPanel> activePanels = GetActivePanels(equipmentPanels);
            
            foreach (EquipmentPanel panel in activePanels)
            {
                AttackType attackType = GetAttackType(panel);

                if (_equipment.ContainsKey(attackType))
                {
                    throw new ArgumentException(nameof(panel));
                }

                panel.SetEnable(false);
                _equipment.Add(attackType, panel);
            }

            foreach (EquipButton button in buttons)
            {
                button.Pressed
                    .Subscribe(Switch)
                    .AddTo(this);
            }
        }

        private List<EquipmentPanel> GetActivePanels(IEnumerable<EquipmentPanel> equipmentPanels)
        {
            List<EquipmentPanel> activePanels = new();
            
            foreach (EquipmentPanel panel in equipmentPanels)
            {
                AttackType attackType = GetAttackType(panel);
                ImplantView implantView = panel.Get();

                
                // if (YG2.saves.SettedImplants.ContainsKey(attackType) == false)
                // {
                //     throw new KeyNotFoundException(nameof(attackType));
                // }
                //
                // if (YG2.saves.SettedImplants[attackType] == implantView) // замокать
                // {
                //     activePanels.Add(panel);
                // }
                if (_mockImplants.ContainsKey(attackType) == false)
                {
                    throw new KeyNotFoundException(nameof(attackType));
                }
                
                if (_mockImplants[attackType] == implantView)
                {
                    activePanels.Add(panel);
                }
            }

            if (activePanels.Count == 0)
            {
                throw new ArgumentException(nameof(equipmentPanels));
            }

            return activePanels;
        }

        private void Switch(EquipmentPanel panel)
        {
            AttackType attackType = GetAttackType(panel);

            if (_equipment.ContainsKey(attackType) == false)
            {
                throw new KeyNotFoundException(nameof(attackType));
            }

            panel.SetEnable(false);
            
            _equipment[attackType].SetEnable(true);
            _equipment[attackType] = panel;
        }

        private AttackType GetAttackType(EquipmentPanel panel)
        {
            ImplantView implantView = panel.Get();
            return implantView.AttackImplant.Parameters.RequiredState;
        }
    }
}