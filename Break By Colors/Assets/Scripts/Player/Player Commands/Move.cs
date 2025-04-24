using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : ICommand
{
    //private field for the players speed
    private float _speed;

    //property for the player's speed value
    public float Speed
    {
        get { return _speed; }
        set { _speed = Mathf.Clamp(value, 0.1f, 10f); }
    }

    private bool _canMove = true;

    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }

    private float _moveTime;
    private PlayerController _pC;
    private MonoBehaviour _mono;
    private Invoker _invoker;


    //class constructor
    public Move(PlayerController pC, float s, float mT, MonoBehaviour m, Invoker i)
    {
        _pC = pC;
        Speed = s;
        _moveTime = mT;
        _mono = m;
        _invoker = i;
    }

    public void Execute()
    {

    }

    public void Execute(InputAction.CallbackContext context, Vector2 swipeDirection)
    {
        if (context.performed)
        {
            _invoker.DisableInvoker();
            _mono.StartCoroutine(MoveLerp(_moveTime, swipeDirection));
        }
    }

    private IEnumerator MoveLerp(float moveTime, Vector2 swipeDirection)
    {
        Vector3 currentPos = _pC.transform.position;
        Vector3 targetPosition = CalculateDistanceToMove(swipeDirection);
        float time = 0f;

        while (time < moveTime)
        {
            _pC.transform.position = Vector3.Lerp(currentPos, targetPosition, time / moveTime);

            time += Time.deltaTime;
            yield return null;
        }

        _pC.transform.position = targetPosition;

        _invoker.EnableInvoker();
    }

    private IEnumerator MoveToCenter(float moveTime)
    {
        Vector3 currentPos = _pC.transform.position;
        Vector3 targetPosition = Vector3.zero;
        float time = 0f;

        while (time < moveTime)
        {
            _pC.transform.position = Vector3.Lerp(currentPos, targetPosition, time / moveTime);

            time += Time.deltaTime;
            yield return null;
        }

        _pC.transform.position = targetPosition;
    }

    /// <summary>
    /// Calculates the distance the player controller needs to move when a left/rigth button is pressed
    /// </summary>
    /// <returns> the movement distance left to move </returns>
    private Vector3 CalculateDistanceToMove(Vector2 swipeDirection)
    {
        Vector3 currentPos = _pC.transform.position;
        Vector3 distance = new Vector3(2f, 0f, 0f);


        if(currentPos == new Vector3(2f, _pC.transform.position.y, 0f))
        {
            if (swipeDirection.x > 0f) //swipe right
            {
                distance = new Vector3(2f, _pC.transform.position.y, 0f);
            }
            else if (swipeDirection.x < 0f) //swipe left
            {
                distance = new Vector3(0f, _pC.transform.position.y, 0f);
            }
        }
        else if(currentPos == new Vector3(-2f, _pC.transform.position.y, 0f))
        {
            if (swipeDirection.x > 0f) //swipe right
            {
                distance = new Vector3(0f, _pC.transform.position.y, 0f);
            }
            else if (swipeDirection.x < 0f) //swipe left
            {
                distance = new Vector3(-2f, _pC.transform.position.y, 0f);
            }
        }
        else
        {
            if (swipeDirection.x > 0f) //swipe right
            {
                distance = new Vector3(2f, _pC.transform.position.y, 0f);
            }
            else if (swipeDirection.x < 0f) //swipe left
            {
                distance = new Vector3(-2f, _pC.transform.position.y, 0f);
            }
        }

        //if the player swipes right on the x axis
        //if (currentPos.x == 2f) //right
        //{
        //    if (swipeDirection.x > 0f) //swipe right
        //    {
        //        distance = new Vector3(0f, 0f, 0f);
        //    }
        //    else if(swipeDirection.x < 0f) //swipe left
        //    {
        //        distance = new Vector3(-2f, 0f, 0f);
        //    }
        //}
        //else if(currentPos.x == -2f) //left
        //{
        //    if (swipeDirection.x > 0f) //swipe right
        //    {
        //        distance = new Vector3(2f, 0f, 0f);
        //    }
        //    else if (swipeDirection.x < 0f) //swipe left
        //    {
        //        distance = new Vector3(0f, 0f, 0f);
        //    }
        //}
        //else //middle
        //{
        //    if (swipeDirection.x > 0f) //swipe right
        //    {
        //        distance = new Vector3(2f, 0f, 0f);
        //    }
        //    else if(swipeDirection.x < 0f)//swipe left
        //    {
        //        distance = new Vector3(-2f, 0f, 0f);
        //    }
        //}

        return distance;
    }

    public void ResestPosition()
    {
        _mono.StartCoroutine(MoveToCenter(_moveTime));
    }
}
