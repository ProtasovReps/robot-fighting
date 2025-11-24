using System.Collections.Generic;
using UI.Panel;
using UI.Store;
using UnityEngine;
using YG.Saver;

namespace UI.Customization
{
    public class EquipmentBag : MonoBehaviour
    {
        private readonly List<EquipmentPanel> _equipmentPanels = new();

        [SerializeField] private Transform _placeHolder;
        [SerializeField] private EquipmentPanel _prefab;

        public IEnumerable<EquipmentPanel> EquipmentPanels => _equipmentPanels;
        
        public void Initialize(GoodSaver goodSaver)
        {
            foreach (ImplantView view in goodSaver.ImplantViews)
            {
                EquipmentPanel panel = Instantiate(_prefab, _placeHolder);
                
                panel.Set(view);
                _equipmentPanels.Add(panel);
            }
        }
    }
}