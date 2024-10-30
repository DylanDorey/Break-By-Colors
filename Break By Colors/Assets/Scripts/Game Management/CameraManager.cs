using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 offsetValue;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float smoothSpeed = 0.1f;

    private Vector3 defaultPosition = new Vector3(0f, 5f, -10f);
    private Vector3 settingsPosition = new Vector3(-10f, 5f, -10f);

    private Vector3 desiredPosition;
    private Vector3 moveToPosition;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.gameLaunch, ReturnToCenter);
        GameEventBus.Subscribe(GameState.returnToMenu, ReturnToCenter);
        GameEventBus.Subscribe(GameState.settingsMenu, MoveToSettingsPosition);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.gameLaunch, ReturnToCenter);
        GameEventBus.Subscribe(GameState.returnToMenu, ReturnToCenter);
        GameEventBus.Unsubscribe(GameState.settingsMenu, MoveToSettingsPosition);
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    /// <summary>
    /// Moves the camera to the settings menu location with a slight delay
    /// </summary>
    public void MoveCamera()
    {
        //desiredPosition = target.position + offsetValue;
        moveToPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = moveToPosition;
    }

    public void ReturnToCenter()
    {
        desiredPosition = defaultPosition + offsetValue;
    }

    public void MoveToSettingsPosition()
    {
        desiredPosition = settingsPosition + offsetValue;
    }
}
