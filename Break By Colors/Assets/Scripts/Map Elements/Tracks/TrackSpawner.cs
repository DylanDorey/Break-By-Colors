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
    public bool inTutorial;

    //reference to the drone object pool
    public TrackObjectPool pool;

    private void OnEnable()
    {
        TrackEventBus.Subscribe(TrackEvent.changeSpeed, UpdateTrackSpeed);

        GameEventBus.Subscribe(GameState.gameLaunch, LaunchSpawnTrack);

        GameEventBus.Subscribe(GameState.returnToMenu, SpawnTrack);

        GameEventBus.Subscribe(GameState.startGame, SpawnTrack);
        GameEventBus.Subscribe(GameState.startGame, StartMoving);

        GameEventBus.Subscribe(GameState.gameOver, StopMoving);
        GameEventBus.Subscribe(GameState.gameOver, ResetTrack);
    }

    private void OnDisable()
    {
        TrackEventBus.Unsubscribe(TrackEvent.changeSpeed, UpdateTrackSpeed);

        GameEventBus.Unsubscribe(GameState.gameLaunch, LaunchSpawnTrack);

        GameEventBus.Unsubscribe(GameState.returnToMenu, SpawnTrack);

        GameEventBus.Unsubscribe(GameState.startGame, SpawnTrack);
        GameEventBus.Unsubscribe(GameState.startGame, StartMoving);

        GameEventBus.Unsubscribe(GameState.gameOver, StopMoving);
        GameEventBus.Unsubscribe(GameState.gameOver, ResetTrack);
    }

    public void SpawnTrack()
    {
        if (!GameManager.Instance.tutorialSetting)
        {
            pool.DestroyTutorialTrack();
            pool.SpawnTutorialTrack();
            pool.DestroyTrackPool();
            inTutorial = true;
        }
        else
        {
            LaunchSpawnTrack();
            pool.DestroyTutorialTrack();
        }
    }

    private void LaunchSpawnTrack()
    {
        if (!GameManager.Instance.tutorialSetting)
        {
            pool = gameObject.GetComponent<TrackObjectPool>();
            pool.SpawnTutorialTrack();
            inTutorial = true;
        }
        else
        {
            pool = gameObject.GetComponent<TrackObjectPool>();
            pool.SpawnGameTrack();
            inTutorial = false;
        }
    }

    /// <summary>
    /// Destroys the track pool and resets it
    /// </summary>
    private void ResetTrack()
    {
        if(!inTutorial)
        {
            pool.DestroyTrackPool();
        }
        else
        {
            pool.DestroyTutorialTrack();
        }

        LaunchSpawnTrack();
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
