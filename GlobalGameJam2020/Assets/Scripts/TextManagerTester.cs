using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManagerTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextManager.Instance.LoadSequence("textBlock0.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
