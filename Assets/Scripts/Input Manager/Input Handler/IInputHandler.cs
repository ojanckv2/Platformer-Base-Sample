using System;
using UnityEngine;

public interface IInputHandler
{
    void Initialize();
    event Action OnLeftInput;
    event Action OnRightInput;
    event Action OnJumpInput;
    event Action OnAttackInput;
}
