using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    public float StartDuration;
    [HideInInspector]
    public float TimeRemaining;
    public bool TimerIsRunning = false;
    public Text TimeText;

    private void Start()
    {
        TimeRemaining = StartDuration;
        TimeText.gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        // Starts the timer automatically
        TimerIsRunning = true;
        TimeText.gameObject.SetActive(true);
    }
    void Update()
    {
        if (TimerIsRunning)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                DisplayTime(TimeRemaining);
            }
            else
            {
                //Debug.Log("Time has run out!");
                GameStateHandler.Instance.LoseGame();
                TimeRemaining = 0;
                TimerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}