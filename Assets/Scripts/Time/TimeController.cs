﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static Action<bool> OnTimeChanged;
    private bool isStopped = false;

    private void Awake()
    {
        PlayerStatusController.OnChangeStatus += OnChangeStatus;
    }

    private void OnChangeStatus(PlayerStatus obj)
    {
        StopTime();
        StartCoroutine(ChangeStatusEnumerator());
    }

    private IEnumerator ChangeStatusEnumerator()
    {
        yield return new WaitForSecondsRealtime(1);
        ContinueTime();
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
            ChangeTimeScale();
    }
}
