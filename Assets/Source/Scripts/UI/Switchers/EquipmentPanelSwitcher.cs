using System;
using System.Collections.Generic;
using Extensions;
using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Panel;
using UI.Store;
using UnityEngine;
using YG.Saver;

namespace UI.Switchers
{
    public class EquipmentPanelSwitcher : MonoBehaviour
    {
        private readonly Dictionary<AttackType, EquipmentPanel> _equipment = new();

        private PlayerImplantSave _implantSave;
        
        [Inject]
        private void Inject(PlayerImplantSave implantSave)
        {
            _implantSave = implantSave;
        }
        
        public void Initialize(IEnumerable<EquipmentPanel> equipmentPanels, IEnumerable<EquipButton> buttons)
        {
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
                
                if (_implantSave.Get(attackType) == implantView)
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