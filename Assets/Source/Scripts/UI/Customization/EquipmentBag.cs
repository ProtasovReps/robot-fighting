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
        [SerializeField] private ImplantView[] _implantViews;    
        
        public IEnumerable<EquipmentPanel> EquipmentPanels => _equipmentPanels;
        
        public void Initialize(ImplantSaver implantSaver)
        {
            for (int i = 0; i < _implantViews.Length; i++)
            {
                ImplantView view = _implantViews[i];

                if (implantSaver.Contains(view) == false)
                {
                    continue;
                }
                    
                EquipmentPanel panel = Instantiate(_prefab, _placeHolder);
                
                panel.Set(view);
                _equipmentPanels.Add(panel);
            }
        }
    }
}