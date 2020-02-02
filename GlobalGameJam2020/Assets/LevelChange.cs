using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private string key;
    

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<TempPlayer>().EnableInteract(key);

            //Director.Instance.Interact(key, other.GetComponent<TempPlayer>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<TempPlayer>().EnableInteract(key);
        }
    }
}
