using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorticoKey : MonoBehaviour
{
    public void LoadScene()
    {
        Director.Instance.Interact("Livingroom", null);
    }
}
