using System.Collections;
using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public bool IsUntouchable { get; private set; }
    public bool IsSuper { get; private set; }
    public PlayerStatus Status { get; private set; }
    public static Action<PlayerStatus> OnChangeStatus;
    [SerializeField] private AudioClip deathTheme;
    [SerializeField] private AudioClip becomeSmallClip;
    [SerializeField] private PlayerSizeController playerSizeAspet;
    [SerializeField] private float superFormTime = 12f;
    [SerializeField] private float untouchableTime = 2f;

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
        StartCoroutine(InvincibleTime(untouchableTime));
        AudioManager.Instance.PlaySound(becomeSmallClip);
        Status = PlayerStatus.Small;
        OnChangeStatus.Invoke(Status);
        playerSizeAspet.setSmallCollider();
    }

    public void BecomeSuper()
    {
        var lastStatus = Status;
        if (Status == PlayerStatus.Small)
        {
            Status = PlayerStatus.SuperSmall;
            OnChangeStatus.Invoke(Status);
            StartCoroutine(TimeForSuperForm(superFormTime, lastStatus));
        }else
        {
            Status = PlayerStatus.SuperBig;
            OnChangeStatus.Invoke(Status);
            StartCoroutine(TimeForSuperForm(superFormTime, lastStatus));
        }

        playerSizeAspet.setBigCollider();
    }

    public void BecomeDead()
    {
        ScoreController.Instance.DecreaseLives();
        AudioManager.Instance.PlayMusic(deathTheme);
        Status = PlayerStatus.Dead;
        OnChangeStatus.Invoke(Status);
    }

    public void LoadDeathScreen()
    {
        SceneController.Instance.LoadDeathScreen();
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
        IsUntouchable = true;
        yield return new WaitForSeconds(invicibleTime);
        IsUntouchable = false;
    }

    IEnumerator TimeForSuperForm(float superFormTime, PlayerStatus lastStatus)
    {
        IsSuper = true;
        yield return new WaitForSeconds(superFormTime);
        Status = lastStatus;
        AudioManager.Instance.PlayMainTheme();
        IsSuper = false;
    }
}
