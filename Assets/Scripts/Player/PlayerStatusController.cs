using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool IsInvincible { get; private set; }

    public PlayerStatus Status { get; private set; }

    public static Action<PlayerStatus> OnChangeStatus;

    public void BecomeBig()
    {
        if (Status != PlayerStatus.Small)
            return;
        Status = PlayerStatus.Big;
        if (OnChangeStatus != null)
            OnChangeStatus.Invoke(Status);
    }
}
