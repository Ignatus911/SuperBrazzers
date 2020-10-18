using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField] private AudioClip bigClip;

    public bool IsInvincible { get; private set; }

    public PlayerStatus Status { get; private set; }

    public static Action<PlayerStatus> OnChangeStatus;

    public void BecomeBig()
    {
        if (Status != PlayerStatus.Small)
            return;
        AudioManager.Instance.Play(bigClip);
        Status = PlayerStatus.Big;
        if (OnChangeStatus != null)
            OnChangeStatus.Invoke(Status);
    }
}
