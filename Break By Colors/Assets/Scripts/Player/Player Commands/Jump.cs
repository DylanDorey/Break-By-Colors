using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : ICommand
{
    private PlayerController _pC;
    private Vector3 _jumpPosition;
    private float _moveTime;
    private float _airStallTime;
    private MonoBehaviour _mono;
    private Invoker _invoker;

    //constructor
    public Jump(PlayerController pC, Vector3 jP, float mT, float aS, MonoBehaviour mB, Invoker i)
    {
        _pC = pC;
        _jumpPosition = jP;
        _moveTime = mT;
        _airStallTime = aS;
        _mono = mB;
        _invoker = i;
    }

    public void Execute()
    {

    }

    public void Execute(InputAction.CallbackContext context, Vector2 swipeDirection)
    {
        //when the button is pressed for the jump command, and the player's y (upward, downward) velocity is equal to 0, execute the jump command
        if (context.performed)
        {
            _mono.StartCoroutine(JumpRoutine(_moveTime));
        }
    }

    private IEnumerator JumpRoutine(float moveTime)
    {
        Vector3 currentPos = _pC.transform.position;
        Vector3 targetPosition = new Vector3(_pC.transform.position.x, _jumpPosition.y, _pC.transform.position.z);
        float time = 0f;
        float duration = moveTime / 1.5f;

        _invoker.DisableInvoker();


        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            _pC.transform.position = Vector3.Lerp(currentPos, targetPosition, t);

            time += Time.deltaTime;
            yield return null;
        }

        _pC.transform.position = targetPosition;

        yield return new WaitForSeconds(_airStallTime);

        time = 0f;
        duration *= 1.5f;

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            _pC.transform.position = Vector3.Lerp(targetPosition, currentPos, t);

            time += Time.deltaTime;
            yield return null;
        }

        _pC.transform.position = currentPos;

        _invoker.EnableInvoker();
    }
}
