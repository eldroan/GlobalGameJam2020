using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private GameObject creditsScreen;

    [SerializeField]
    private GameObject settingsScreen;

    bool a = false;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(Constants.AudioClips.SAD_PIANO);
        buttons[0].Select();
    }

    public void Play()
    {
        SceneManager.LoadScene(Constants.Scenes.GAME_SCENE);
    }

    public void Settings()
    {
        settingsScreen.SetActive(true);
    }

    public void ShowCredits()
    {
        //TODO: lo que queramos hacer
        if (a == false)
            AudioManager.Instance.PlaySpecialFX(Constants.AudioClips.TYPEWRITER);
        else
            AudioManager.Instance.StopSpecialFX();
        a = !a;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
