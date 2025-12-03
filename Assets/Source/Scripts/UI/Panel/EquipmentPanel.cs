using TMPro;
using UI.Buttons;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Panel
{
    [RequireComponent(typeof(EquipButton))]
    public class EquipmentPanel : SwitchablePanel
    {
        [SerializeField] private Image _sellableImage;
        [SerializeField] private Image _sellableType;
        [SerializeField] private TMP_Text _name;
        
        private ImplantView _implantView;

        public void Set(ImplantView implantView)
        {
            _sellableImage.sprite = implantView.ImplantImage;
            _sellableType.sprite = implantView.ImplantTypeImage;
            _name.text = implantView.Name;

            _implantView = implantView;
        }

        public ImplantView Get()
        {
            return _implantView;
        }
    }
}