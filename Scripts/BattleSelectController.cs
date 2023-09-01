using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSelectController : MonoBehaviour
{
    private void Start()
    {
        if(AudioManager.instance.bgMusicPlaying != null)
        {
            AudioManager.instance.bgMusicPlaying.Stop();
        }

        AudioManager.instance.PlayMusic(1);
    }
    private void Update()
    {

    }
    public void RandomAI()
    {
        AudioManager.instance.PlaySoundFx(4);

        if (AudioManager.instance.musicPlaying != null)
        {
            AudioManager.instance.musicPlaying.Stop();
        }

        AudioManager.instance.PlayRandomBGMusic();
        SceneManager.LoadScene(2);
    }
    public void DeffensiveAI()
    {
        AudioManager.instance.PlaySoundFx(4);

        if (AudioManager.instance.musicPlaying != null)
        {
            AudioManager.instance.musicPlaying.Stop();
        }

        AudioManager.instance.PlayRandomBGMusic();
        SceneManager.LoadScene(3);
    }
    public void OffensiveAI()
    {
        AudioManager.instance.PlaySoundFx(4);

        if (AudioManager.instance.musicPlaying != null)
        {
            AudioManager.instance.musicPlaying.Stop();
        }

        AudioManager.instance.PlayRandomBGMusic();
        SceneManager.LoadScene(4);
    }
}
