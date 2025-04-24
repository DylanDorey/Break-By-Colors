using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/08/2024]
 * [A controller that allows the player to move left, right, and jump]
 */

public class PlayerController : Singleton<PlayerController>
{
    [Range(0.1f, 1.0f)]
    [Tooltip("The time before another movement can be made")]
    public float movementDelay;

    [Range(0f, 10f)]
    [Tooltip("The time before another movement can be made")]
    public float moveSpeed;

    [SerializeField]
    private Transform _playerJumpPosition;

    public Rigidbody rb;

    [SerializeField]
    private Transform playerModelTransform;

    private Vector2 swipeDirection;

    private Player playerActionMap;

    private readonly Invoker _movementInvoker = new Invoker();
    private Move _move;
    private Jump _jump;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.startGame, EnableInput);
        GameEventBus.Subscribe(GameState.loadGame, DisableInput);
        GameEventBus.Subscribe(GameState.gameOver, DisableInput);
        GameEventBus.Subscribe(GameState.returnToMenu, DisableInput);
        GameEventBus.Subscribe(GameState.returnToMenu, ResetPlayerPosition);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.startGame, EnableInput);
        GameEventBus.Unsubscribe(GameState.loadGame, DisableInput);
        GameEventBus.Unsubscribe(GameState.gameOver, DisableInput);
        GameEventBus.Unsubscribe(GameState.returnToMenu, DisableInput);
        GameEventBus.Unsubscribe(GameState.returnToMenu, ResetPlayerPosition);
    }

    private void Start()
    {
        InitializePlayerController();
    }

    void FixedUpdate()
    {
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
        swipeDirection = context.ReadValue<Vector2>();

        //if the player swipes further/more on the x axis
        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            _movementInvoker.InvokeMovement(_move, context, swipeDirection);
        }
        //otherwise, if the player swipes further/more on the y axis
        else
        {
            //
            if (swipeDirection.y > 0.5f)
            {
                if (CheckIfGrounded())
                {
                    _movementInvoker.InvokeMovement(_jump, context, swipeDirection);
                }
            }
        }
    }

    /// <summary>
    /// Moves the player left
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        _movementInvoker.InvokeMovement(_move, context, value);
    }

    /// <summary>
    /// Moves the player upwards
    /// </summary>
    /// <param name="context"> the state of the input recieved </param>
    public void OnJump(InputAction.CallbackContext context)
    {
        _movementInvoker.InvokeMovement(_jump, context, swipeDirection);
    }

    /// <summary>
    /// Checks if the player is on the ground
    /// </summary>
    public bool CheckIfGrounded()
    {
        //if the raycast hits something
        if (Physics.Raycast(transform.position, Vector3.down, 0.7f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Rotates the players model at the speed of the track
    /// </summary>
    private void BallRotate()
    {
        playerModelTransform.Rotate(1f, .7f, .5f);
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
        playerActionMap.PlayerMovement.Swipe.performed += OnSwipeEnded;
        playerActionMap.PlayerMovement.Swipe.performed += OnMove;

        //initialize the player's rigidbody component
        rb = GetComponent<Rigidbody>();

        _move = new Move(this, moveSpeed, movementDelay, this, _movementInvoker);
        _jump = new Jump(this, _playerJumpPosition.position, movementDelay, 0.3f, this, _movementInvoker);

        transform.position = Vector3.zero;
    }

    /// <summary>
    /// Resets the players position back to the starting location
    /// </summary>
    public void ResetPlayerPosition()
    {
        _move.ResestPosition();

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

    public void DisableInput()
    {
        _movementInvoker.DisableInvoker();
    }

    public void EnableInput()
    {
        _movementInvoker.EnableInvoker();
    }
}
