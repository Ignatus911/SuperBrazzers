using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJump : MonoBehaviour, BoxState
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoLogic()
    {
        animator.SetTrigger("Jump");
        Debug.Log("Just jump");
    }

}
