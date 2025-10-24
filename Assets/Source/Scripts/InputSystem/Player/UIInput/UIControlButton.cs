using System;
using InputSystem;
using Reflex.Attributes;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public abstract class UIControlButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{
    private const float ActiveActionValue = 1f;
    
    protected override string controlPathInternal { get; set; }
    protected abstract int ControlIndex { get; }
    protected abstract string ActionName { get; }

    [Inject]
    private void Inject(UserInput userInput)
    {
        SetupControlPath(userInput);
    }    
    
    public void OnPointerUp(PointerEventData data)
    {
        SendValueToControl(0f);
    }

    public void OnPointerDown(PointerEventData data)
    {
        SendValueToControl(ActiveActionValue);
    }
   
    private void SetupControlPath(UserInput userInput)
    {
        InputActionMap actionMap = userInput.Player.Get();
        InputAction newAction = null;
        
        foreach (InputAction action in actionMap.actions)
        {
            if (action.name == ActionName)
            {
                newAction = action;
                break;
            }
        }

        if (newAction == null)
        {
            throw new ArgumentNullException(nameof(newAction));
        }
        
        string device = newAction.controls[ControlIndex].device.name;
        string actionName = newAction.controls[ControlIndex].name;
        
        controlPathInternal = $"<{device}>/{actionName}";
    }
}