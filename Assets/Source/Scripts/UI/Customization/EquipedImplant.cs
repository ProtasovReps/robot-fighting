using System;
using System.Collections.Generic;
using Extensions;
using R3;
using Reflex.Attributes;
using TMPro;
using UI.Buttons;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;
using YG.Saver;

namespace UI.Customization
{
    public class EquipedImplant : MonoBehaviour
    {
        [SerializeField] private Image _sellableImage;
        [SerializeField] private TMP_Text _sellableName;
        [SerializeField] private ImplantView _tempStartImplantView;  // Temo!!!!
        [SerializeField] private AttackType _requiredState;

        private PlayerImplantSave _playerImplantSave;
        
        [Inject]
        private void Inject(PlayerImplantSave playerImplantSave)
        {
            _playerImplantSave = playerImplantSave;
        }
        
        public void Initialize(List<EquipButton> equipButtons)
        {
            for (int i = 0; i < equipButtons.Count; i++)
            {
                equipButtons[i].Pressed
                    .Where(panel => panel.Get().AttackImplant.Parameters.RequiredState == _requiredState)
                    .Subscribe(implant => Set(implant.Get()))
                    .AddTo(this);
            }
            
            // Set(YG2.saves.SettedImplants[RequiredState]);
            Set(_tempStartImplantView);
        }

        private void Set(ImplantView implantView)
        {
            if (implantView.AttackImplant.Parameters.RequiredState != _requiredState)
                throw new ArgumentException(nameof(implantView));
            
            _playerImplantSave.Set(_requiredState, implantView.AttackImplant);

            _sellableImage.sprite = implantView.SellableImage;
            _sellableName.text = implantView.Name;
        }
    }
}