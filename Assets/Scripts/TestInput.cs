using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{

    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            animator.SetTrigger("run");
        }
        else if (Input.GetKeyDown("s"))
        {
            animator.SetTrigger("idle");
        }
    }
}
