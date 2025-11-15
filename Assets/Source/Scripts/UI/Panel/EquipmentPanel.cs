using TMPro;
using UI.Buttons;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    [RequireComponent(typeof(EquipButton))]
    public class EquipmentPanel : SwitchablePanel
    {
        [SerializeField] private Image _sellableImage;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _name;
        
        private ImplantView _implantView;

        public void Set(ImplantView implantView)
        {
            _sellableImage.sprite = implantView.SellableImage;
            _background.sprite = implantView.Background;
            _name.text = implantView.Name;

            _implantView = implantView;
        }

        public ImplantView Get()
        {
            return _implantView;
        }
    }
}