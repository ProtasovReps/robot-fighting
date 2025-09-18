using UnityEngine;

namespace InputSystem.UIInput
{
    public class ActionButton : UIControlButton
    {
        [SerializeField] private ActionName _actionName;
        
        protected override int ControlIndex => 0;
        protected override string ActionName => _actionName.ToString();
    }
}