using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip music;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private GameObject creditsScreen;

    [SerializeField]
    private GameObject settingsScreen;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(music);
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
    }

    public void Quit()
    {
        Application.Quit();
    }
}
