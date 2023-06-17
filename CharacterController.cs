using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private AudioSource aSource;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        animator.SetTrigger("TrIdle");
        aSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        animator.SetTrigger("TrHover");
        aSource.Play();
        aSource.volume = 1f;
    }

    private void OnMouseExit()
    {
        animator.SetTrigger("TrIdle");
        aSource.Stop();
    }
}
