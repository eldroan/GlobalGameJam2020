using UnityEngine;

public class CameraController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //PlayStartGameAnimation();
    }

    public void PlayStartGameAnimation()
    {
        animator.SetTrigger("StartGame");
    }
}
