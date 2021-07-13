///-----------------------------------------------------------------------------
///
/// GameTimer
/// 
/// Timer realted
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public class GameTimer
    {
        public static float timeElapsed = 0;// seconds
        bool isTimerRunning = false;

        public GameTimer()
        {
            isTimerRunning = false;
        }


        public void Update()
        {
            if (isTimerRunning)
                timeElapsed += Time.deltaTime;
        }


        public string GetTimeAsString()
        {
            return timeElapsed.ToString("f2");
        }


        public int GetTimeInSeconds()
        {
            return (int)timeElapsed;
        }


        public void StartTimer()
        {
            isTimerRunning = true;
            timeElapsed = 0;
        }


        public void PauseTimer()
        {
            isTimerRunning = false;
        }


        public void ResumeTimer()
        {
            isTimerRunning = true;
        }


        public void StopTimer()
        {
            isTimerRunning = false;
        }


        public void OnLevelStarted()
        {
            isTimerRunning = true;
        }


        public void OnGamePaused()
        {
            isTimerRunning = false;
        }


        public void OnGameResumed()
        {
            isTimerRunning = true;
        }
    }

    // 2do
    // ToString("f2")
    // ToString(@"mm/:ss/:fff")
    // format 00:00
    // Countdown timer
    // Stop Watch
    // Events : v=1hsppNzx7_0
    // Bullet fire rate ?
    // Reload Timer ?
    // Coroutine vs Timer
    // Destroy timer ?
    //

}