using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static Action<bool> OnTimeChanged;
    public static Action<bool> OnPauseGame;
    private bool isStopped = false;
    private static bool playerSetPause = false;
    public static bool PlayerSetPause => playerSetPause;

    private void Awake()
    {
        PlayerStatusController.OnChangeStatus += OnChangeStatus;
    }

    private void OnDestroy()
    {
        PlayerStatusController.OnChangeStatus -= OnChangeStatus;
        playerSetPause = false;
    }

    private void OnChangeStatus(PlayerStatus status)
    {
        StopTime();
        StartCoroutine(ChangeStatusEnumerator(status));
    }

    private IEnumerator ChangeStatusEnumerator(PlayerStatus status)
    {
        switch (status)
        {
            case (PlayerStatus.Dead):
                yield return new WaitForSecondsRealtime(3);
                ContinueTime();
                SceneController.Instance.LoadDeathScreen();
                break;
            default:
                yield return new WaitForSecondsRealtime(1);
                ContinueTime();
                break;
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0;
        OnTimeChanged?.Invoke(false);
    }

    public void ContinueTime()
    {
        Time.timeScale = 1;
        OnTimeChanged?.Invoke(true);
    }

    public void ChangeTimeScale()
    {
        isStopped = !isStopped;
        if (isStopped)
            StopTime();
        else
            ContinueTime();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerSetPause = !playerSetPause;
            ChangeTimeScale();
            OnPauseGame(isStopped);
        }
    }
}
