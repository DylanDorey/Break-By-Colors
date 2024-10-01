using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/15/2024]
 * [A track spawner that spawns track game objects from the track object pool]
 */

public class TrackSpawner : Singleton<TrackSpawner>
{
    [Range(15f, 50f)] public float startSpeed;
    [Range(1, 30)] public int trackLength;

    [Tooltip("! MUST MATCH THE LENGTH OF THE TRACK PREFAB !")]
    public int trackSize;

    [Tooltip("The likeliness that a wall will not spawn")]
    [Range(2, 5)] public int wallSpawnChance;

    [Tooltip("The speed at which the track increases speed over time")]
    [Range(0.01f, 10f)]
    public float speedAccelerationMultiplier;

    public bool moving = false;

    //reference to the drone object pool
    private TrackObjectPool pool;

    private void OnEnable()
    {
        TrackEventBus.Subscribe(TrackEvent.changeSpeed, UpdateTrackSpeed);

        GameEventBus.Subscribe(GameState.mainMenu, StartSpawning);
        GameEventBus.Subscribe(GameState.startGame, StartMoving);
        GameEventBus.Subscribe(GameState.gameOver, StopMoving);
        GameEventBus.Subscribe(GameState.returnToMenu, ResetTrack);
    }

    private void OnDisable()
    {
        TrackEventBus.Unsubscribe(TrackEvent.changeSpeed, UpdateTrackSpeed);

        GameEventBus.Unsubscribe(GameState.mainMenu, StartSpawning);
        GameEventBus.Unsubscribe(GameState.startGame, StartMoving);
        GameEventBus.Unsubscribe(GameState.gameOver, StopMoving);
        GameEventBus.Unsubscribe(GameState.returnToMenu, ResetTrack);
    }

    private void StartSpawning()
    {
        //initialize the track object pool
        if (gameObject.GetComponent<TrackObjectPool>())
        {
            pool = gameObject.GetComponent<TrackObjectPool>();
            pool.Spawn();
        }
        else
        {
            Debug.LogError("ERROR: No track object pool found!");
        }
    }

    private void ResetTrack()
    {
        pool.DestroyTrackPool();
    }

    public void StartMoving()
    {
        moving = true;
    }

    public void StopMoving()
    {
        moving = false;
    }

    /// <summary>
    /// Updates the speed of the tracks
    /// </summary>
    public void UpdateTrackSpeed()
    {
        foreach(GameObject trackObject in pool.trackPool)
        {
            Track track = trackObject.GetComponent<Track>();
            track.SetSpeed(track.GetSpeed() + speedAccelerationMultiplier);
        }
    }
}
