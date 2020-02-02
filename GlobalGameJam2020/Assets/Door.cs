using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string key;
    
    [SerializeField] private Renderer render;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player")) {
            render.material.SetFloat("_Outline", 5);
            other.GetComponent<TempPlayer>().EnableInteract(key);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player")) {
            render.material.SetFloat("_Outline", 0);
            other.GetComponent<TempPlayer>().DisableInteract();

        }
    }
}
