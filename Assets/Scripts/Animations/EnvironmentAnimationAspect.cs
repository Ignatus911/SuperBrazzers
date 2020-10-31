using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimationAspect : MonoBehaviour
{

    private Animator selfAnimation;

    private void Awake()
    {
        selfAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        selfAnimation.SetBool( "ah", true);
    }
}
