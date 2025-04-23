using UnityEngine;
using UnityEngine.InputSystem;

public class Invoker
{
    private bool _enabled = true;

    public bool Enabled
    {
        get { return _enabled; }
    }
    /// <summary>
    /// Executes a command given a movement command and the context in which the command must be executed
    /// </summary>
    /// <param name="iC">the command interface that the invoker is implementing</param>
    /// <param name="context">the context in which the command should happen</param>
    public void InvokeMovement(ICommand iC, InputAction.CallbackContext context, Vector2 swipeDirection)
    {
        if (_enabled)
        {
            iC?.Execute(context, swipeDirection);
        }
    }

    /// <summary>
    /// Executes the command that is called
    /// </summary>
    /// <param name="iF"></param>
    public void Invoke(ICommand iC)
    {
        if (_enabled)
        {
            iC?.Execute();
        }
    }

    public void DisableInvoker()
    {
        _enabled = false;
    }

    public void EnableInvoker()
    {
        _enabled = true;
    }
}
