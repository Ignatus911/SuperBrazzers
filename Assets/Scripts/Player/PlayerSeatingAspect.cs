using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeatingAspect : MonoBehaviour
{
    [SerializeField] private InputControl input;
    [SerializeField] private PlayerSizeController sizeController;
    [SerializeField] private PlayerMovementAspect movementAspect;
    public PlayerStatusController playerStatus;

    public bool IsSeating => input.IsSeatKeyPressed;

    // Update is called once per frame
    void Update()
    {
        if (playerStatus.Status == PlayerStatus.Big)
        {
            if (IsSeating)
            {
                sizeController.setSmallCollider();
                Debug.Log("small");
                return;
            }
            else sizeController.setBigCollider();
        }
    }
}
