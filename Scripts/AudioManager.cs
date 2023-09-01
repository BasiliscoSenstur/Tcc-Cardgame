using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ------------------------------------ //
    public AudioSource[] music, bgMusic, soundFx;
    public AudioSource bgMusicPlaying;
    public AudioSource musicPlaying;

    void Start()
    {

    }

    void Update()
    {

    }
    
    public void PlayMusic(int audio)
    {
        if (music[audio].isPlaying == false)
        {
            foreach(AudioSource track in music)
            {
                track.Stop();
            }
            music[audio].Play();

            musicPlaying = music[audio];
        }
    }

    public void PlaySoundFx(int sfx)
    {
        soundFx[sfx].Stop();
        soundFx[sfx].Play();
    }

    //public void PlaySoundFx(int audio)
    //{

    //    if (soundFx[audio].isPlaying == false)
    //    {
    //        foreach (AudioSource track in soundFx)
    //        {
    //            track.Stop();
    //        }
    //        soundFx[audio].Play();
    //    }
    //}

    public void PlayRandomBGMusic()
    {
        int audio = Random.Range(0, bgMusic.Length);

        if (bgMusic[audio].isPlaying == false)
        {
            foreach (AudioSource track in bgMusic)
            {
                track.Stop();
            }
            bgMusic[audio].Play();
        }
        bgMusicPlaying = bgMusic[audio];
    }
}
