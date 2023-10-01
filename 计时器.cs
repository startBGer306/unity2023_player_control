using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class 计时器 : MonoBehaviour
{
    private float countdownTimerStartTime;
    private int countdownTimerDuration;
    public float TimeOfNew()
    {
        countdownTimerStartTime = Time.time;
        return countdownTimerStartTime;  
    }
    public int CountdownTime()
    {
        int elapsedSeconds = (int)(Time.time - countdownTimerStartTime);
        int secondsLeft = (countdownTimerDuration - elapsedSeconds);
        return secondsLeft;
    }
    public void ResetTime(int countdowntime)
    {
        countdownTimerStartTime = Time.time;
        countdownTimerDuration = countdowntime;
    }
}
