using Ojanck.Core.Scene;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : SceneService
{
    [SerializeField] private DekstopInputHandler dekstopInputHandler;

    public UnityEvent onLeftInput = new();
    public UnityEvent onRightInput = new();
    public UnityEvent onJumpInput = new();
    public UnityEvent onAttackInput = new();

    private bool canInput = true;
    public bool CanInput
    {
        get => canInput;
        set => canInput = value;
    }

    protected override void OnInitialize()
    {
        dekstopInputHandler.Initialize();

        RegisterCallback(dekstopInputHandler);
    }

    private void RegisterCallback(params IInputHandler[] inputHandlers)
    {
        foreach (var inputHandler in inputHandlers)
        {
            inputHandler.OnLeftInput += InvokeLeftInput;
            inputHandler.OnRightInput += InvokeRightInput;
            inputHandler.OnJumpInput += InvokeJumpInput;
            inputHandler.OnAttackInput += InvokeAttackInput;
        }
    }

    private void InvokeLeftInput()
    {
        if (!canInput) return;
        onLeftInput?.Invoke();
    }

    private void InvokeRightInput()
    {
        if (!canInput) return;
        onRightInput?.Invoke();
    }

    private void InvokeJumpInput()
    {
        if (!canInput) return;
        onJumpInput?.Invoke();
    }
    
    private void InvokeAttackInput()
    {
        if (!canInput) return;
        onAttackInput?.Invoke();
    }
}