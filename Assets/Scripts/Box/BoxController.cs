using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private BoxLogic logic;
    [SerializeField] private GameObject checker;
    [SerializeField] private float checkerSize = 0.8f;
    [SerializeField] private BoxDestroy boxDestroy;
    [SerializeField] private BoxJump boxJump;
    [SerializeField] private PlayerStatusController playerStatus;
    private BoxState currentState;

    // Update is called once per frame
    void Update()
    {
        var HitByPlayer = Physics2D.OverlapBox(checker.transform.position, new Vector2 (checkerSize, Mathf.Epsilon), 0f, LayerMask.GetMask("Player"));
        if (HitByPlayer)
        {
            if (playerStatus.IsBig)
                currentState = boxDestroy;
            else currentState = boxJump;

            currentState.DoLogic();
        }
    }
}
