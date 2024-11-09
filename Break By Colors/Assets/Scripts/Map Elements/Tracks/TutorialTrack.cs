using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrack : MonoBehaviour
{
    //speed of the track
    private readonly float speed = 18;

    private void FixedUpdate()
    {
        if (TrackSpawner.Instance.moving)
        {
            Move();
        }
    }

    /// <summary>
    /// Allows the track to move in a continous direction
    /// </summary>
    private void Move()
    {
        //move along the z axis towards the player at a particular speed
        transform.Translate(0f, 0f, (-speed * Time.deltaTime));
    }
}
