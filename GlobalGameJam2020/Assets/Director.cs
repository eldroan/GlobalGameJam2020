using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public string Key;
        public GameObject LevelObject;
        public bool Active;
    }

    public static Director Instance { get; private set;}

    [SerializeField] private Level[] levels;
    [SerializeField] private Item[] items;

    private bool interactuoConAlfombra;
    private bool haveDrawerKey = false;

    private void Awake() {
        Instance = this;
    }

    private void Start()
    {
        var s = GameObject.Find("SogaInteractor");
        var s2 = GameObject.Find("SisterInteractor2");

        s.GetComponent<Collider2D>().enabled = false;
        s2.GetComponent<Collider2D>().enabled = false;

        foreach (var lvl in levels)
        {
            if (lvl.LevelObject != null)
                lvl.LevelObject.SetActive(lvl.Active);
        }
    }

    public void ChangeAct()
    {
        var s = GameObject.Find("SogaInteractor");
        var s2 = GameObject.Find("SisterInteractor2");
        var s3 = GameObject.Find("SisterInteractor");

        s.GetComponent<Collider2D>().enabled = true;
        s2.GetComponent<Collider2D>().enabled = true;
        s3.GetComponent<Collider2D>().enabled = false;

        foreach (var lvl in levels)
        {
            lvl.LevelObject.SetActive(lvl.Key == "Atico");
            lvl.Active = lvl.Key == "Atico";
        }
    }

    public void Interact(string key, TempPlayer player)
    {
        switch(key) {
            case "Atico":
                activeLevel("Atico", player);
            break;
            case "Mother_bedroom":
                activeLevel("Mother_bedroom", player);
            break;
            case "Livingroom":
                activeLevel("Livingroom", player);
            break;
            case "Alma_bedroom":
                TextManager.Instance.LoadSequence(player, "");
            break;
            case "Hall":
                activeLevel("Hall", player);
            break;
            case "Inventory":
                Inventory.Instance.ShowHidde();
            break;
            case "Sister_bedroom":
                activeLevel("Sister_bedroom", player);
            break;
            case "Sister_bedroom2":
                activeLevel("Sister_bedroom2", player);
            break;
            case "TV":
                interactTV(player);
            break;
            case "Atic_Box":
                TextManager.Instance.LoadSequence(player, "");
            break;
            case "Picture":
                TextManager.Instance.LoadSequence(player, "");
            break;
            case "Drawer":
                if (haveDrawerKey)
                {
                    AudioManager.Instance.PlayFX("Drawer");
                    //TODO: Puzzle
                }
                else
                {
                    TextManager.Instance.LoadSequence(player, "");
                }
                break;
            case "Key":
                interactPorticoKey(player);
            break;
            case "Dog":
                interactDog(player);
            break;
            case "Alfombra":
                //Texto de acá había una llave
                interactuoConAlfombra = true;
                TextManager.Instance.LoadSequence(player,"");
            break;
        }
    }

    private void interactTV(TempPlayer player) {
        var living = GameObject.Find("Living");
        AudioManager.Instance.PlayFX("Susto");
        living.GetComponent<RandomizeColor>().TurnOff();
        player.AlowInteracting();

    }

    private void interactPorticoKey(TempPlayer player)
    {
        var living = GameObject.Find("Portico");

        living.GetComponent<PorticoGameplay>().Play();
        //player.AlowInteracting();
    }

    private void interactDog(TempPlayer player) {
        if(interactuoConAlfombra)
        {
            var living = GameObject.Find("Portico");

            living.GetComponent<PorticoGameplay>().PararPerro();
            //player.AlowInteracting();
        }
        else
        {
            AudioManager.Instance.PlayFX("Dog");
        }
        player.AlowInteracting();
    }

    private void activeLevel(string levelName, TempPlayer player) {
        var currentLvl = levels.Select(x => x).FirstOrDefault(x => x.Active == true);

        currentLvl.Active = false;
        currentLvl.LevelObject.SetActive(false);

        var newLvl = levels.Select(x => x).FirstOrDefault(x => x.Key == levelName);
        newLvl.Active = true;
        newLvl.LevelObject.SetActive(true);

        player?.AlowInteracting();
    }
}
