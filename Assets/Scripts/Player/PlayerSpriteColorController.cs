using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteColorController : MonoBehaviour
{
    [SerializeField] private PlayerStatusController status;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private float changeColorCourutine = 0.2f;
    [SerializeField] private float changeTransparencyCourutine = 0.3f;
    [SerializeField] private float transparency = 0.2f;
    private float currentCourutineTime;
    private Color defaultColor;
    private Color defaultTransparency;
    private Color currentColor;
    private Color currentTransparency;


    private void Awake()
    {
        defaultColor = renderer.color;
        currentColor = defaultColor;
    }

    private void Update()
    {
        if (status.IsSuper)
        {
            ChangeColor();
        }
        else if (status.IsUntouchable)
        {
            ChangeTransparency();
        }
        else
            renderer.color = defaultColor;
    }

    private void ChangeColor()
    {
        if (currentCourutineTime <= 0)
        {
            renderer.color = Random.ColorHSV();
            currentCourutineTime = changeColorCourutine;
        }
        else currentCourutineTime -= Time.deltaTime;
        return;
    }

    private void ChangeTransparency()
    {
        if (currentCourutineTime <= 0)
        {
            currentColor = setTransparency(currentColor);
            renderer.color = currentColor;
            currentCourutineTime = changeTransparencyCourutine;
        }
        else currentCourutineTime -= Time.deltaTime;
        return;
    }

    private Color setTransparency(Color color)
    {
        if (color == defaultColor)
            color.a = transparency;
        else color = defaultColor;
        return color;
    }
}
