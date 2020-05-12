using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private float previousFrame;
    private float currentFrame;

    public void Tick()
    {
        previousFrame = currentFrame;
        currentFrame += Time.deltaTime;
    }

    public void ResetTimer()
    {
        previousFrame = 0;
        currentFrame = 0;
    }

    public bool AtTime(float time)
    {
        return currentFrame > time && previousFrame <= time;
    }

    public float GetCurrentTime() { return currentFrame; }


    /// <summary>
    /// Returns true if previous or current frame is within the range
    /// </summary>
    /// <returns></returns>
    public bool DuringTime(float startTime, float endTime)
    {
        return currentFrame > startTime && previousFrame < endTime;
    }

    
    /// <summary>
    /// Returns true if previousFrame is past the given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool PassedTime(float time)
    {
        return previousFrame > time;
    }


    /// <summary>
    /// Returns the amount of time between the currentFrame and the given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public float TimePast(float time)
    {
        return currentFrame - time;
    }
}

