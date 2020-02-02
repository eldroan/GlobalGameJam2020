﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeColor : MonoBehaviour
{
    [SerializeField] private GameObject[] image;
    [SerializeField] private float deltaTime = 0.2f;

    private float time;

    // Update is called once per frame
    void Update()
    {
        if (time >= deltaTime)
        {
            float r = Random.value;
            float g = Random.value;
            float b = Random.value;
            foreach(var obj in image)
                obj.GetComponent<SpriteRenderer>().color = new Color(r, g, b, 0.6f);
            time = 0;
        }
        else
            time += Time.deltaTime;
    }

    public void TurnOff()
    {
        image[0].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0f);
        image[1].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    }
}
