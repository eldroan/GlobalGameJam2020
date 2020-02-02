﻿using System.Collections;
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
    
    private void Awake() {
        Instance = this;
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
                activeLevel("Alma_bedroom", player);
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
                
            break;
            case "Picture":
                
            break;
            case "Drawer":
                
            break;
            case "Dog":
                interactDog(player);
            break;
        }
    }

    private void interactTV(TempPlayer player) {
        var living = GameObject.Find("Living");

        living.GetComponent<RandomizeColor>().TurnOff();
        player.AlowInteracting();

    }

    private void interactDog(TempPlayer player) {
        var living = GameObject.Find("Portico");

        living.GetComponent<PorticoGameplay>().PararPerro();
        player.AlowInteracting();
    }

    private void activeLevel(string levelName, TempPlayer player) {
        var currentLvl = levels.Select(x => x).FirstOrDefault(x => x.Active == true);

        currentLvl.Active = false;
        currentLvl.LevelObject.SetActive(false);

        var newLvl = levels.Select(x => x).FirstOrDefault(x => x.Key == levelName);
        newLvl.Active = true;
        newLvl.LevelObject.SetActive(true);

        player.AlowInteracting();
    }
}
