using System.Collections.Generic;
using UI.Panel;
using UI.Store;
using UnityEngine;

namespace UI.Customization
{
    public class EquipmentBag : MonoBehaviour
    {
        private readonly List<EquipmentPanel> _equipmentPanels = new();

        [SerializeField] private Transform _placeHolder;
        [SerializeField] private EquipmentPanel _prefab;
        [SerializeField] private ImplantView[] _sellables; // temp, получать из Initializer

        public IEnumerable<EquipmentPanel> EquipmentPanels => _equipmentPanels;
        
        public void Initialize()
        {
            for (int i = 0; i < _sellables.Length; i++)
            {
                EquipmentPanel panel = Instantiate(_prefab, _placeHolder);
                
                panel.Set(_sellables[i]);
                _equipmentPanels.Add(panel);
            }
        }
    }
}