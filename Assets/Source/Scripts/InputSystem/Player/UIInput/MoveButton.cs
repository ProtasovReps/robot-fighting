using System;
using UnityEngine;

namespace InputSystem.UIInput
{
    public class MoveButton : UIControlButton
    {
        private const string Move = nameof(Move);
        
        [SerializeField] private bool _isBackMove;

        protected override int ControlIndex => Convert.ToInt32(_isBackMove);
        protected override string ActionName => Move;
    }
}