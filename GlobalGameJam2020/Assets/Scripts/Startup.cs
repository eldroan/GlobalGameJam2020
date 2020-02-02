using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TODO: cuando termine una animación o video.
    }

    public void OnSplashFinished()
    {
        PuzzleManager.Instance.gameObject.transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene(Constants.Scenes.MENU_SCENE);
    }
}
