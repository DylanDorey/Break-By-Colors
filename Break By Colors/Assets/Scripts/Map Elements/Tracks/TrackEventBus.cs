using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [09/28/2024]
 * [Subscribes, Unsubsribes, and Publishes Track events based upon the players actions]
 */

//the various track events
public enum TrackEvent
{
    //changeSpeed
}

public class TrackEventBus : MonoBehaviour
{
    //Initialize a dictionary of track events
    private static readonly IDictionary<TrackEvent, UnityEvent> Events = new Dictionary<TrackEvent, UnityEvent>();

    /// <summary>
    /// Adds a listener to a specific track event
    /// </summary>
    /// <param name="eventType"> the specific track event </param>
    /// <param name="listener"> the function/method getting added to the track event</param>
    public static void Subscribe(TrackEvent eventType, UnityAction listener)
    {
        //the event
        UnityEvent thisEvent;

        //if the function is assigned to the specific unity event
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            //add it as a listener
            thisEvent.AddListener(listener);
        }
        else
        {
            //otherwise it is a new listener
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    /// <summary>
    /// Removes a listener from a specific track event
    /// </summary>
    /// <param name="type"> the specific track event </param>
    /// <param name="listener"> the function/method getting removed from the track event </param>
    public static void Unsubscribe(TrackEvent type, UnityAction listener)
    {
        //the event
        UnityEvent thisEvent;

        //if the function is equal to the event being removed
        if (Events.TryGetValue(type, out thisEvent))
        {
            //remove the function from the list of listeners
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Establishes the current event happening
    /// </summary>
    /// <param name="type"> the specific event </param>
    public static void Publish(TrackEvent type)
    {
        //the event
        UnityEvent thisEvent;

        //Invoke the various functions for the event
        if (Events.TryGetValue(type, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
