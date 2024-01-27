using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController
{
    private TimerMB _timerMB;
    private float _time;
    private float _speedTime = 1;
    private int _preSec;
    private Action _newSecAction;
    public void Init(TimerMB timerMB)
    {
        _timerMB = timerMB;
        _preSec = (int)_time;

        SetTime();
    }

    public void Update() 
    {
        _time += Time.deltaTime * _speedTime;

        if((int)_time > _preSec)
        {
            _preSec = (int)_time;
            _newSecAction?.Invoke();
            SetTime();
        }
    }

    public void SetSecTickAction(Action action)
    {
        _newSecAction += action;
    }

    private void SetTime()
    {
        _timerMB.TMP.text = $"{(int)(_time / 60)} : {(int)(_time % 60)}";
    }
}