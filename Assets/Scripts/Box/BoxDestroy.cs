using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroy : MonoBehaviour, BoxState
{
    public void DoLogic()
    {
        StartCoroutine(BoxExistCoroutine());
        Debug.Log("Destroyed");
    }

    IEnumerator BoxExistCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
