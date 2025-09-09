using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DekstopInputHandler : MonoBehaviour, IInputHandler
{
    private bool isInitialized = false;

    public event Action OnLeftInput;
    public event Action OnRightInput;
    public event Action OnJumpInput;
    public event Action OnAttackInput;

    [SerializeField] private InputActionAsset inputActionAsset;
    private InputActionMap playerInput;

    public void Initialize()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (isInitialized) return;

        playerInput = inputActionAsset.FindActionMap("Player Input");
        playerInput.Enable();

        isInitialized = true;
#endif
    }

    private void Update()
    {
        if (!isInitialized) return;

        if (playerInput.FindAction("Left").IsPressed())
            OnLeftInput?.Invoke();
        else if (playerInput.FindAction("Right").IsPressed())
            OnRightInput?.Invoke();

        if (playerInput.FindAction("Jump").WasPressedThisFrame())
            OnJumpInput?.Invoke();

        if (playerInput.FindAction("Attack").WasPressedThisFrame())
            OnAttackInput?.Invoke();
    }
}
