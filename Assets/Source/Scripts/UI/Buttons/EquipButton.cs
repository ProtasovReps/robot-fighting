using UI.Panel;
using UnityEngine;

namespace UI.Buttons
{
    public class EquipButton : ObservableButton<EquipmentPanel>
    {
        [SerializeField] private EquipmentPanel _equipmentPanel;
        
        protected override EquipmentPanel Get()
        {
            return _equipmentPanel;
        }
    }
}