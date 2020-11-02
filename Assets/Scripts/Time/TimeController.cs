using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static Action<bool> OnTimeChanged;
    public static Action<bool> OnPauseGame;
    private bool isStopped = false;
    [SerializeField] PlayerStatusController playerStatus;
    [SerializeField] private float timeForLVLPass = 400;
    [SerializeField] private float timerRatio = 2.5f;
    private float currentTime;

    private void Awake()
    {
        PlayerStatusController.OnChangeStatus += OnChangeStatus;
        currentTime = timeForLVLPass;
    }

    private void OnDestroy()
    {
        PlayerStatusController.OnChangeStatus -= OnChangeStatus;
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
    //убивать с анимацией, звуком, таймер запускать после загрузки уровня.
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeTimeScale();
            OnPauseGame(isStopped);
        }
        if (isStopped) return;
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime * timerRatio;
            ScoreController.Instance.WriteCurrentTime((int)currentTime);
        }
        else
        {
            //SceneController.Instance.LoadDeathScreen(); / нет визуализации смерти
            //playerStatus.BecomeDead(); //не правильно, умирает бесконечно вечно
        }
    }
}
