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
        SceneManager.LoadScene(Constants.Scenes.MENU_SCENE);
    }
}
