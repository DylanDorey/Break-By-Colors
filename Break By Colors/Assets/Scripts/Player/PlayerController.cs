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
    private Vector3 targetPos = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private bool hasMoved = false;

    [Range(0.0f, 1.0f)]
    [Tooltip("How fast the player controller moves")]
    public float movementDampaner;

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
            //if the player hasnt moved, and the players x position is currently greater than -0.1
            if (!hasMoved && transform.position.x > -0.1f)
            {
                //add the desired movement distance to the target position and add movement delay
                targetPos = transform.position + moveLeftDistance;
                StartCoroutine(MovementDelay());
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
            //if the player hasnt moved, and the players x position is currently less than 0.1
            if (!hasMoved && transform.position.x < 0.1f)
            {
                //add the desired movement distance to the target position and add movement delay
                targetPos = transform.position + moveRightDistance;
                StartCoroutine(MovementDelay());
            }
        }
    }

    /// <summary>
    /// Adds a delay between each movement made by the player
    /// </summary>
    /// <returns> time between movements </returns>
    private IEnumerator MovementDelay()
    {
        for (int index = 0; index < 1; index++)
        {
            //set hasMoved to true and wait 0.9 seconds
            hasMoved = true;

            yield return new WaitForSeconds(0.9f);
        }

        //set hasMoved back to false
        hasMoved = false;
    }
}
