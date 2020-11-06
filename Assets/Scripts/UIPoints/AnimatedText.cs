using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedText : MonoBehaviour
{
    private RectTransform selfTransform;

    private void Awake()
    {
         selfTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector3 position)
    {
        selfTransform.position = position;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Debug.Log("selfTransform = " + selfTransform.position.x);
        Debug.Log("RectTransform = " + GetComponent<RectTransform>().position.x);
    }
}
