using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool IsInvincible { get; private set; }
    public PlayerStatus Status { get; private set; }
    public static Action<PlayerStatus> OnChangeStatus;

    [SerializeField] private PlayerSizeAspet playerSizeAspet;

    public void BecomeBig()
    {
        if (Status != PlayerStatus.Small)
            return;
        Status = PlayerStatus.Big;
        if (OnChangeStatus != null)
            OnChangeStatus.Invoke(Status);
        playerSizeAspet.setBigCollider();
    }


    public void Hit()
    {
        //Если большой то мелкий
        //если мелкий то умри
        playerSizeAspet.setSmallCollider();
    }
}
