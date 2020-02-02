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
    private Image logo;

    [SerializeField]
    private CameraController cameraController;

    private bool onCredits = false;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(Constants.AudioClips.SAD_PIANO);
        buttons[0].Select();
    }

    public void Play()
    {
        //cameraController.PlayStartGameAnimation();
        SceneManager.LoadScene(Constants.Scenes.GAME_SCENE);
    }

    public void ShowCredits()
    {
        if (onCredits)
        {
            logo.gameObject.SetActive(true);
            creditsScreen.SetActive(false);
            onCredits = false;
        }
        else
        {
            logo.gameObject.SetActive(false);
            creditsScreen.SetActive(true);
            onCredits = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
