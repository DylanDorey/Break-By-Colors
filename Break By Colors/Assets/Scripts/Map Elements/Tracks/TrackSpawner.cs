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
    [Range(1, 5)] public int wallSpawnChance;

    [Tooltip("The max speed the track can move at")]
    [Range(1, 100)] public int maxTrackSpeed;

    [Tooltip("The speed at which the track increases speed over time")]
    [SerializeField]
    private float speedAccelerationMultiplier;

    public bool moving = false;

    //reference to the drone object pool
    public TrackObjectPool pool;

    private void OnEnable()
    {
        TrackEventBus.Subscribe(TrackEvent.changeSpeed, UpdateTrackSpeed);

        GameEventBus.Subscribe(GameState.gameLaunch, LaunchSpawn);

        GameEventBus.Subscribe(GameState.returnToMenu, StartSpawning);

        GameEventBus.Unsubscribe(GameState.startGame, StartSpawning);
        GameEventBus.Subscribe(GameState.startGame, StartMoving);

        GameEventBus.Subscribe(GameState.gameOver, StopMoving);
        GameEventBus.Subscribe(GameState.gameOver, ResetTrack);
    }

    private void OnDisable()
    {
        TrackEventBus.Unsubscribe(TrackEvent.changeSpeed, UpdateTrackSpeed);

        GameEventBus.Unsubscribe(GameState.gameLaunch, LaunchSpawn);

        GameEventBus.Unsubscribe(GameState.returnToMenu, StartSpawning);

        GameEventBus.Unsubscribe(GameState.startGame, StartSpawning);
        GameEventBus.Unsubscribe(GameState.startGame, StartMoving);

        GameEventBus.Unsubscribe(GameState.gameOver, StopMoving);
        GameEventBus.Unsubscribe(GameState.gameOver, ResetTrack);
    }

    private void StartSpawning()
    {
        //if(GameManager.Instance.tutorialSetting)
        //{
        //    ResetTrack();
        //    pool = gameObject.GetComponent<TrackObjectPool>();
        //    pool.SpawnTutorialTrack(0);
        //}

        pool = gameObject.GetComponent<TrackObjectPool>();
        pool.SpawnGameTrack();
    }

    private void LaunchSpawn()
    {
        pool = gameObject.GetComponent<TrackObjectPool>();
        pool.SpawnGameTrack();
    }

    /// <summary>
    /// Destroys the track pool and resets it
    /// </summary>
    private void ResetTrack()
    {
        pool.DestroyTrackPool();
    }

    public void StartMoving()
    {
        StartCoroutine(MoveDelay());
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
        //if the tracks speed is less than the max track speed
        if (pool.trackPool[0].GetComponent<Track>().GetSpeed() < maxTrackSpeed)
        {
            //set each track's speed value to the new speed
            foreach (GameObject trackObject in pool.trackPool)
            {
                Track track = trackObject.GetComponent<Track>();
                track.SetSpeed(track.GetSpeed() + speedAccelerationMultiplier);
            }
        }
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1.5f);

        moving = true;
    }
}
