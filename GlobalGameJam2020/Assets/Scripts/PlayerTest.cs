using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    ParticleSystem myParticleSystem;
    Animator animator;
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        myParticleSystem.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myParticleSystem.Play();
            animator.SetTrigger("Possess");
        }
    }
}
