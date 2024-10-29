using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/08/2024]
 * [A controller that allows the player to move left, right, and jump]
 */

public enum MoveDirection
{
    Left = -2,
    Right = 2
};

public class PlayerController : Singleton<PlayerController>
{
    [Range(0.0f, 1.0f)]
    [Tooltip("How fast the player controller moves")]
    public float movementDampaner;

    [Range(0.0f, 1.0f)]
    [Tooltip("The time before another movement can be made")]
    public float movementDelay;

    public Rigidbody rb;
    //private Vector3 moveLeftDistance = new Vector3(-2f, 0f, 0f);
    //private Vector3 moveRightDistance = new Vector3(2f, 0f, 0f);
    public Vector3 targetPos = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private bool hasMoved = false;
    public bool swiped = false;

    [SerializeField]
    private bool isGrounded = true;

    private Transform playerModelTransform;

    private Vector2 swipeDirection;

    private Player playerActionMap;

    //property for the current move direction of the player
    public MoveDirection CurrentMoveDirection
    {
        get; private set;
    }

    private IPlayerState moveState;

    //reference to the context of the player state
    private PlayerStateContext playerStateContext;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.returnToMenu, ResetPlayerPosition);
    }

    private void OnDisable()
    { 
        GameEventBus.Unsubscribe(GameState.returnToMenu, ResetPlayerPosition);
    }

    private void Start()
    {
        InitializePlayerController();
    }

    void FixedUpdate()
    {
        //move in the direction of the current x and y movement values * players speed
        transform.position = Vector3.Lerp(transform.position, targetPos, movementDampaner);

        CheckIfGrounded();
        BallRotate();
    }

    /// <summary>
    /// Stores the direction of the swipe into a vector2 when a swipe is started
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnSwipePerformed(InputAction.CallbackContext context)
    {
        swipeDirection = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// When the player lifts their finger after a swipe they will traverse in the desired direction
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnSwipeEnded(InputAction.CallbackContext context)
    {
        //if the player swipes further/more on the x axis
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            //if the player swipes right on the x axis
            if (swipeDirection.x > 0f)
            {
                //if the player hasnt moved, and the players x position is currently less than 0.1, and isGrounded
                if (!hasMoved && transform.position.x < 0.1f && isGrounded)
                {
                    //set the turn direction to the direction that was called
                    CurrentMoveDirection = MoveDirection.Right;

                    //transition the player's state
                    playerStateContext.Transition(moveState);

                    //add movement delay
                    StartCoroutine(MovementDelay());
                }
            }
            //if the player swipes left on the x axis
            else if (swipeDirection.x < 0f)
            {
                //if the player hasnt moved, and the players x position is currently greater than -0.1, and isGrounded
                if (!hasMoved && transform.position.x > -0.1f && isGrounded)
                {
                    //set the turn direction to the direction that was called
                    CurrentMoveDirection = MoveDirection.Left;

                    //transition the player's state
                    playerStateContext.Transition(moveState);

                    //add movement delay
                    StartCoroutine(MovementDelay());
                }
            }
        }
        ////otherwise, if the player swipes further/more on the y axis
        //else
        //{
        //    //if the player swipes up on the y axis
        //    if (swipeDirection.y > 0)
        //    {
        //        //if the player isGrounded
        //        if (isGrounded)
        //        {
        //            //add jump force to the player
        //            rb.AddForce((Vector3.up * 20f), ForceMode.Impulse);
        //        }
        //    }
        //}
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
            if (!hasMoved && transform.position.x > -0.1f && isGrounded)
            {
                //set the turn direction to the direction that was called
                CurrentMoveDirection = MoveDirection.Left;

                //transition the player's state
                playerStateContext.Transition(moveState);

                //add movement delay
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
            if (!hasMoved && transform.position.x < 0.1f && isGrounded)
            {
                //set the turn direction to the direction that was called
                CurrentMoveDirection = MoveDirection.Right;

                //transition the player's state
                playerStateContext.Transition(moveState);

                //add movement delay
                StartCoroutine(MovementDelay());
            }
        }
    }

    /// <summary>
    /// Moves the player upwards
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnJump(InputAction.CallbackContext context)
    {
        //if the input was performed
        if (context.performed)
        {
            if (isGrounded)
            {
                rb.AddForce((Vector3.up * 60f), ForceMode.Impulse);
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

            yield return new WaitForSeconds(movementDelay);
        }

        //set hasMoved back to false
        hasMoved = false;
    }

    /// <summary>
    /// Checks if the player is on the ground
    /// </summary>
    private void CheckIfGrounded()
    {
        //if the raycast hits something
        if (Physics.Raycast(transform.position, Vector3.down, 0.7f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    /// <summary>
    /// Rotates the players model at the speed of the track
    /// </summary>
    private void BallRotate()
    {
        playerModelTransform.Rotate(TrackSpawner.Instance.pool.trackPool[0].gameObject.GetComponent<Track>().GetSpeed() / 20f, .7f, .5f);
    }

    /// <summary>
    /// Initializes all player controller components and values
    /// </summary>
    private void InitializePlayerController()
    {
        //create and enable a new player action map
        playerActionMap = new Player();
        playerActionMap.Enable();

        //Store the correct functions for when a swipe is performed/started and a touch is canceled/lifted
        playerActionMap.PlayerMovement.Swipe.performed += OnSwipePerformed;
        playerActionMap.PlayerMovement.Touch.canceled += OnSwipeEnded;


        //initialize the player's rigidbody component
        rb = GetComponent<Rigidbody>();

        //the player model's transform
        playerModelTransform = transform.GetChild(0).transform;

        //intialize the player context object
        playerStateContext = new PlayerStateContext(this);

        //initialize the player move state intefaces
        moveState = gameObject.AddComponent<PlayerMoveState>();
    }

    /// <summary>
    /// Resets the players position back to the starting location
    /// </summary>
    public void ResetPlayerPosition()
    {
        targetPos = Vector3.zero;

        StartCoroutine(DisableCollider());
    }

    /// <summary>
    /// Disables the players collider for a specific duration
    /// </summary>
    /// <returns> the duration the collider is disabled for </returns>
    private IEnumerator DisableCollider()
    {
        for (int index = 0; index < 1; index++)
        {
            rb.useGravity = false;
            GetComponent<SphereCollider>().enabled = false;

            yield return new WaitForSeconds(1f);
        }

        GetComponent<SphereCollider>().enabled = true;
        rb.useGravity = true;
    }
}
