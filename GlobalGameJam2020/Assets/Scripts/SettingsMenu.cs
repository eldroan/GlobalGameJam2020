﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].Select();
    }

}
