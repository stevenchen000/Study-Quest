using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audio;
    private AudioSource source;

    private void Awake()
    {
        if(audio == null)
        {
            audio = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Plays an audio clip once
    /// </summary>
    /// <param name="clip"></param>
    public static void PlayClip(AudioClip clip)
    {
        audio.source.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays a looping song
    /// </summary>
    /// <param name="song"></param>
    public static void PlaySong(AudioClip song)
    {
        audio.source.clip = song;
        audio.source.Play();
    }
}
