using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/08/2024]
 * [A controller that allows the player to move left, right, and jump]
 */

public class PlayerController : MonoBehaviour
{
    private Vector3 moveLeftDistance = new Vector3(-1.5f, 0f, 0f);
    private Vector3 moveRightDistance = new Vector3(1.5f, 0f, 0f);
    private Vector3 targetPos;
    private bool hasMoved = false;

    [Range(0.0f, 1.0f)]
    [Tooltip("How fast the player controller moves")]
    public float movementDampaner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //move in the direction of the current x and y movement values * players speed
        transform.position = Vector3.Lerp(transform.position, targetPos, movementDampaner);
    }

    /// <summary>
    /// Moves the player left
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        //if the input was performed
        if (context.performed)
        {
            if (!hasMoved && transform.position.x > -0.1f)
            {
                targetPos = transform.position + moveLeftDistance;
            }
        }
    }

    /// <summary>
    /// Moves the player right
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        //if the input was performed
        if (context.performed)
        {
            if (!hasMoved && transform.position.x < 0.1f)
            {
                targetPos = transform.position + moveRightDistance;
            }
        }
    }
}
