using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] private float xSpeed = 2f;
    [SerializeField] private float ySpeed = 10f;

    private bool enabledInteraction;
    private string keyInteract;
    private bool showInventory = false;
    private Vector3 lastPosition;
    private ParticleSystem myParticleSystem;
    private Animator animator;

    private bool interacting = false;

    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        myParticleSystem.Stop();
    }

    private void Update()
    {        
        if (Input.GetButtonDown("Interact") && enabledInteraction) {
            interacting = !interacting;
            Director.Instance.Interact(keyInteract, this);
        }

        if (!interacting) {        
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

            var horizontal = moveHorizontal * xSpeed * Time.deltaTime;
            var vertical = moveVertical * ySpeed * Time.deltaTime;

            this.transform.position = this.transform.position + (new Vector3(horizontal, vertical, 0));

            var moveY = lastPosition - this.transform.position;

            //this.transform.localScale = this.transform.localScale + (new Vector3(moveY.y, moveY.y, 0));

            lastPosition = this.transform.position;
        }

    }

    public void EnableInteract(string key) 
    {
        this.keyInteract = key;
        enabledInteraction = true;
    }

    public void DisableInteract() 
    {
        this.keyInteract = "";
        enabledInteraction = false;
    }

    public void AlowInteracting() 
    {
        interacting = false;
    }
    
    public void Possess()
    {
        myParticleSystem.Play();
        animator.SetTrigger("Possess");
    }
}
