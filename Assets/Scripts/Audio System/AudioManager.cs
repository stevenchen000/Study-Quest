using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audio;
    private AudioSource source;
    [SerializeField]
    private AudioClip backgroundMusic;
    private float backgroundMusicCurrentTime = 0;

    private void Awake()
    {
        if(audio == null)
        {
            audio = this;
            DontDestroyOnLoad(this);
            source = transform.GetComponent<AudioSource>();
            source.clip = backgroundMusic;
            source.Play();
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
        SaveBackgroundMusicTime();
        audio.source.PlayOneShot(clip);

    }

    /// <summary>
    /// Plays the song
    /// Stops the current background music, but does not override it
    /// </summary>
    /// <param name="song"></param>
    public static void PlaySong(AudioClip song)
    {
        SaveBackgroundMusicTime();
        audio.source.clip = song;
        audio.source.Play();
    }

    /// <summary>
    /// Changes the background music
    /// </summary>
    /// <param name="song"></param>
    public static void ChangeBackgroundMusic(AudioClip song){
        if(audio.source.clip != song){
            audio.source.clip = song;
            audio.backgroundMusic = song;
            audio.backgroundMusicCurrentTime = 0;
            audio.source.Play();
        }
    }

    /// <summary>
    /// Resumes background music from when it was last paused
    /// </summary>
    public static void ResumeBackgroundMusic(){
        audio.source.clip = audio.backgroundMusic;
        audio.source.time = audio.backgroundMusicCurrentTime;
        audio.source.Play();
    }

    /// <summary>
    /// Stops the background music
    /// </summary>
    public static void StopMusic(){
        SaveBackgroundMusicTime();
        audio.source.Stop();
    }

    private static void SaveBackgroundMusicTime(){
        if(audio.source.clip == audio.backgroundMusic){
            audio.backgroundMusicCurrentTime = audio.source.time;
        }
    }
}
