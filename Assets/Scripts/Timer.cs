using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _remainingTime;
    public float settedTime;
    public bool isRunning = false;

    public Timer(float newRemainingTime)
    {
        settedTime = newRemainingTime;
        _remainingTime = settedTime;
        isRunning = true;
    }

    public void SetTime(float newRemainigTime)
    {
        if (isRunning == false)
        {
            settedTime = newRemainigTime;
            _remainingTime = settedTime;
            isRunning = true;
        }
    }

    public void ResetTimer()
    {
        if (isRunning == false)
        {
            _remainingTime = settedTime;
            isRunning = true;
        }
    }
    
    void Start()
    {
        isRunning = true;
    }
    
    void Update()
    {
        if (isRunning)
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
            }
            else
            {
                _remainingTime = 0;
                isRunning = false;
            }
        }
    }
}
