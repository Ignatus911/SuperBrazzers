using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroy : MonoBehaviour, BoxState
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void DoLogic()
    {
        StartCoroutine(BoxExistCoroutine());
        Debug.Log("Destroyed");
    }

    IEnumerator BoxExistCoroutine()
    {
        animator.SetBool("isExist", false);
        Destroy(GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
