using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/15/2024]
 * [A track that spawns and moves beneath the player controller]
 */

public class Track : MonoBehaviour
{
    //speed of the track
    private float speed;
    private int localTrackSize;
    private int trackLength;
    private Vector3 spawnPos;

    private void FixedUpdate()
    {
        if (TrackSpawner.Instance.startMoving)
        {
            Move();
        }
    }

    /// <summary>
    /// Allows the track to move in a continous direction
    /// </summary>
    private void Move()
    {
        //if the track moves too far behind the player
        if (transform.position.z < -20)
        {
            //go back to the start of the track length
            transform.position = spawnPos;
        }

        //move along the z axis towards the player at a particular speed
        transform.Translate(0f, 0f, (-speed * Time.deltaTime));
    }

    /// <summary>
    /// Initializes the track section
    /// </summary>
    /// <param name="trackSize"></param>
    /// <param name="totalTrackLength"></param>
    /// <param name="trackSpeed"></param>
    public void InitializeTrack(int trackSize, int totalTrackLength, float startSpeed)
    {
        localTrackSize = trackSize;
        trackLength = totalTrackLength;
        speed = startSpeed;

        spawnPos = new Vector3(0f, -0.7f, (localTrackSize * trackLength) - (localTrackSize + 20));
    }
}
