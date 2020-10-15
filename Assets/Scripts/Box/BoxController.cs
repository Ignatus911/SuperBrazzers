using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private BoxType boxType;
    [SerializeField] private GameObject checker;
    [SerializeField] private float checkerSize = 0.8f;
    [SerializeField] private BoxDestroy boxDestroy;
    [SerializeField] private BoxJump boxJump;
    [SerializeField] private PlayerStatusController playerStatus;
    private BoxState currentState;
    private bool wait;

    // Update is called once per frame
    void Update()
    {
        var HitByPlayer = Physics2D.OverlapBox(checker.transform.position, new Vector2(checkerSize, Mathf.Epsilon), 0f, LayerMask.GetMask("Player"));
        if (HitByPlayer && !wait)
        {
            switch (boxType)
            {
                case BoxType.SimpleBrick:
                    if (playerStatus.IsBig)
                        currentState = boxDestroy;
                    else currentState = boxJump;
                    break;

                case BoxType.BoxWithCoins:
                    currentState = boxJump;
                    break;

                case BoxType.BoxWithFlower:
                    currentState = boxJump;
                    break;

                case BoxType.BoxWithMushroom:
                    currentState = boxJump;
                    break;
            }
            currentState.DoLogic();
            wait = true;
            StartCoroutine(DontReact());
        }
    }

    IEnumerator DontReact()
    {
        yield return new WaitForSeconds(0.3f);
        wait = false;
    }

}
