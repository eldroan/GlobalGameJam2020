using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public static Director Instance { get; private set;}

    private void Awake() {
        Instance = this;
    }

    public void Interact(string key)
    {
        switch(key) {
            case "Atico":
            break;
            case "Madre":
            break;
            case "Living":
            break;
            case "Alma":
            break;
            case "Inventory":
                Inventory.Instance.ShowHidde();
            break;
            case "Hermana":
            break;
        }
    }
}
