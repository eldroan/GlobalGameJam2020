using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PorticoGameplay : MonoBehaviour
{
    [SerializeField] private Sprite perroParado;
    [SerializeField] private Sprite keySprite;
    [SerializeField] private GameObject perroGo;
    [SerializeField] private GameObject keyGo;
    [SerializeField] private TempPlayer player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PararPerro();
        //Play();
    }

    public void PararPerro()
    {
        perroGo.GetComponent<SpriteRenderer>().sprite = perroParado;
    }

    public void Play()
    {
        keyGo.GetComponent<SpriteRenderer>().sprite = keySprite;
        player.Possess();
        keyGo.GetComponent<Animator>().SetTrigger("move");
    }
}
