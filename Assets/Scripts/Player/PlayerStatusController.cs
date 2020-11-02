using System.Collections;
using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool IsInvincible { get; private set; }
    public PlayerStatus Status { get; private set; }
    public static Action<PlayerStatus> OnChangeStatus;
    [SerializeField] private AudioClip deathTheme;
    [SerializeField] private AudioClip becomeSmallClip;

    [SerializeField] private PlayerSizeController playerSizeAspet;

    public void BecomeBig()
    {
        if (Status != PlayerStatus.Small)
            return;
        Status = PlayerStatus.Big;
        if (OnChangeStatus != null)
            OnChangeStatus.Invoke(Status);
        playerSizeAspet.setBigCollider();
    }

    private void BecomeSmall()
    {
        StartCoroutine(InvincibleTime(2f));
        AudioManager.Instance.PlaySound(becomeSmallClip);
        Status = PlayerStatus.Small;
        OnChangeStatus.Invoke(Status);
        playerSizeAspet.setSmallCollider();
    }

    public void BecomeDead()
    {
        ScoreController.Instance.DecreaseLives();
        AudioManager.Instance.PlayMusic(deathTheme);
        Status = PlayerStatus.Dead;
        OnChangeStatus.Invoke(Status);
    }

    public void Hit()
    {
        if (Status == PlayerStatus.Small)
        {
            BecomeDead();
            //показать количесвто жизней, начать с контрольной точки
            return;
        }
        else if(Status != PlayerStatus.Dead){ BecomeSmall(); }
    }

    IEnumerator InvincibleTime(float invicibleTime)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(invicibleTime);
        IsInvincible = false;
    }
}
