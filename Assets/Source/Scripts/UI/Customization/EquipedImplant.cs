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
        [SerializeField] private AttackType _requiredState;
        [SerializeField] private ImplantView[] _implantViews;
        
        private EquipedImplantSaver _equipedImplantSaver;
        
        [Inject]
        private void Inject(EquipedImplantSaver equipedImplantSaver)
        {
            _equipedImplantSaver = equipedImplantSaver;
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

            for (int i = 0; i < _implantViews.Length; i++)
            {
                ImplantView view = _implantViews[i];
                
                if(_equipedImplantSaver.IsSetted(_requiredState, view) == false)
                    continue;
                
                Set(view);
                break;
            }
        }

        private void Set(ImplantView implantView)
        {
            if (implantView.AttackImplant.Parameters.RequiredState != _requiredState)
                throw new ArgumentException(nameof(implantView));
            
            _equipedImplantSaver.Set(_requiredState, implantView);

            _sellableImage.sprite = implantView.ImplantImage;
            _sellableName.text = implantView.Name;
        }
    }
}