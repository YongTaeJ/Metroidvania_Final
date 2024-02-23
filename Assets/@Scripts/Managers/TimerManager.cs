using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// 인터넷 참고 자료
public class TimerManager : Singleton<TimerManager>
{
    List<Timer> timers = new List<Timer>();
    private void Update()
    {
        for(int i= timers.Count -1; i >= 0; i--)
        {
            timers[i].restTime -= Time.deltaTime;
            if(timers[i].restTime <= 0)
            {
                timers[i]?.OnTime();
                timers.RemoveAt(i);
            }
        }
    }

    public void StartTimer(float time, Action OnTime)
    {
        timers.Add(new Timer(time, OnTime));
    }
}

public class Timer
{
    public Action OnTime;
    public float restTime;
    public Timer(float restTime, Action OnTime)
    {
        this.restTime = restTime;
        this.OnTime = OnTime;
    }
}
