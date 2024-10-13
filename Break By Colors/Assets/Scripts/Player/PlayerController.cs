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

    //[Range(0.0f, 5f)]
    //[Tooltip("How high the player controller jumps upwards")]
    //public float jumpHeight;

    [Range(0.0f, 1.0f)]
    [Tooltip("The time before another movement can be made")]
    public float movementDelay;

    public Rigidbody rb;
    //private Vector3 moveLeftDistance = new Vector3(-2f, 0f, 0f);
    //private Vector3 moveRightDistance = new Vector3(2f, 0f, 0f);
    public Vector3 targetPos = new Vector3(0f, 0f, 0f);

    [SerializeField]
    private bool hasMoved = false;

    [SerializeField]
    private bool isGrounded = true;

    private Transform playerModelTransform;


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
                rb.AddForce((Vector3.up * 35f), ForceMode.Impulse);
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

    private void BallRotate()
    {
        playerModelTransform.Rotate(TrackSpawner.Instance.pool.trackPool[0].gameObject.GetComponent<Track>().GetSpeed(), 0f, 0f);
    }

    /// <summary>
    /// Initializes all player controller components and values
    /// </summary>
    private void InitializePlayerController()
    {
        //initialize the player's rigidbody component
        rb = GetComponent<Rigidbody>();

        playerModelTransform = transform.GetChild(0).transform;

        //intialize the player context object
        playerStateContext = new PlayerStateContext(this);

        //initialize the player move state intefaces
        moveState = gameObject.AddComponent<PlayerMoveState>();
    }

    public void ResetPlayerPosition()
    {
        targetPos = Vector3.zero;

        StartCoroutine(DisableCollider());
    }

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
