using InputSystem;
using Reflex.Attributes;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public abstract class UIControlButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{
    private const float ActiveActionValue = 1f;
    
    private string _сontrolPath;

    [Inject]
    private void Inject(UserInput userInput)
    {
        SetupControlPath(userInput);
    }    
    
    public void OnPointerUp(PointerEventData data)
    {
        SendValueToControl(0.0f);
    }

    public void OnPointerDown(PointerEventData data)
    {
        SendValueToControl(ActiveActionValue);
    }

    protected override string controlPathInternal
    {
        get => _сontrolPath;
        set => _сontrolPath = value;
    }

    protected abstract int ControlIndex { get; }
    protected abstract string ActionName { get; }
    
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

        string device = newAction.controls[ControlIndex].device.name;
        string name = newAction.controls[ControlIndex].name;
        
        _сontrolPath = $"<{device}>/{name}";
    }
}