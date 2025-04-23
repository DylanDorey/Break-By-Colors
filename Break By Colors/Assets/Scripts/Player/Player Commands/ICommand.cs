using UnityEngine;
using UnityEngine.InputSystem;

public interface ICommand
{
    public void Execute();
    public void Execute(InputAction.CallbackContext context, Vector2 swipeDirection);
}
