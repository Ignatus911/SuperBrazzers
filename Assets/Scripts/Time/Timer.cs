using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] PlayerStatusController playerStatus;
    [SerializeField] private float timeForLVLPass = 400;
    [SerializeField] private float timerRatio = 2.5f;
    private PlayerStatus Status;
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
        if(status == PlayerStatus.Dead)
        {
            Destroy(this);
        }
        return;
    }
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime * timerRatio;
            UIController.Instance.WriteCurrentTime((int)currentTime);
        }
        else  playerStatus.BecomeDead();
    }
}
