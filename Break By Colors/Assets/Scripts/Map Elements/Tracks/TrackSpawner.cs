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

    [Range(1, 100)] public int maxTrackSpeed;

    public bool moving = false;

    //reference to the drone object pool
    public TrackObjectPool pool;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.gameLaunch, LaunchSpawnTrack);

        GameEventBus.Subscribe(GameState.returnToMenu, SpawnTrack);

        GameEventBus.Subscribe(GameState.startGame, SpawnTrack);
        GameEventBus.Subscribe(GameState.startGame, StartMoving);

        GameEventBus.Subscribe(GameState.gameOver, StopMoving);
        GameEventBus.Subscribe(GameState.gameOver, ResetTrack);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.gameLaunch, LaunchSpawnTrack);

        GameEventBus.Unsubscribe(GameState.returnToMenu, SpawnTrack);

        GameEventBus.Unsubscribe(GameState.startGame, SpawnTrack);
        GameEventBus.Unsubscribe(GameState.startGame, StartMoving);

        GameEventBus.Unsubscribe(GameState.gameOver, StopMoving);
        GameEventBus.Unsubscribe(GameState.gameOver, ResetTrack);
    }

    public void SpawnTrack()
    {
        if (GameManager.Instance.tutorialSetting)
        {
            pool.DestroyTutorialTrack();
            pool.SpawnTutorialTrack();
            pool.DestroyTrackPool();
        }
        else
        {
            LaunchSpawnTrack();
            pool.DestroyTutorialTrack();
        }
    }

    private void LaunchSpawnTrack()
    {
        if (GameManager.Instance.tutorialSetting)
        {
            pool = gameObject.GetComponent<TrackObjectPool>();
            pool.SpawnTutorialTrack();
        }
        else
        {
            pool = gameObject.GetComponent<TrackObjectPool>();
            pool.SpawnGameTrack();
        }
    }

    /// <summary>
    /// Destroys the track pool and resets it
    /// </summary>
    private void ResetTrack()
    {
        pool.DestroyTrackPool();
        pool.DestroyTutorialTrack();

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
    public void TrackSpeedUpdate(float speedChange)
    {
        //if the tracks speed is less than the max track speed
        if (pool.trackPool[0].GetComponent<Track>().GetSpeed() < maxTrackSpeed)
        {
            //set each track's speed value to the new speed
            foreach (GameObject trackObject in pool.trackPool)
            {
                Track track = trackObject.GetComponent<Track>();
                track.SetSpeed(track.GetSpeed() + speedChange);
            }
        }
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1.5f);

        moving = true;
    }
}
