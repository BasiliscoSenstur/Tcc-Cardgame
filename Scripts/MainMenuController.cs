using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    private void Awake()
    {
        instance = this;
    }
    // ------------------------------------ //
    private void Start()
    {
        if (AudioManager.instance.bgMusicPlaying != null)
        {
            AudioManager.instance.bgMusicPlaying.Stop();
        }

        AudioManager.instance.PlayMusic(0);
    }

    private void Update()
    {

    }

    public void PlayGame()
    {
        AudioManager.instance.PlaySoundFx(4);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySoundFx(4);
        Application.Quit();

        Debug.Log("Quit Game");
    }

}
