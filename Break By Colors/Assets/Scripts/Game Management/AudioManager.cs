using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [10/31/2024]
 * [Processes all audio passed by the unique game objects]
 */

public class AudioManager : Singleton<AudioManager>
{
    //the audio sources attached to any game object
    public AudioSource desiredAudioSource;

    /// <summary>
    /// Plays the audio of any game object passed to it if the game audio is on
    /// </summary>
    /// <param name="audioSource"> the source of the audio that needs to be played </param>
    /// <param name="audioClip"> the exact sound/sound clip that needs to be played by the audio source </param>
    public void PlayAudio(AudioSource audioSource, AudioClip audioClip, bool loop)
    {
        //if the game's audio setting is enabled
        if (GameManager.Instance.audioSetting)
        {
            //set the desired source to the passed in audio source
            desiredAudioSource = audioSource;

            //set the desired source's clip to play as the passed in audio clip
            desiredAudioSource.clip = audioClip;

            //if the audio source is a looping audio clip, set the desired audio sources looping parameter to the passed in loop value
            desiredAudioSource.loop = loop;

            //play the audio clip from the audio source
            desiredAudioSource.Play();
        }
    }
}
