using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime
{
    public int Hour { get; set; }
    public int Minute { get; set; }

    public MyTime() { }

    public MyTime SetHour(int hour)
    {
        Hour = hour;
        return this;
    }

    public MyTime SetMinute(int minute)
    {
        Minute = minute;
        return this;
    }

    public override string ToString()
    {
        return Hour.ToString("00") + ":" + Minute.ToString("00");
    }
}

public class ClockGameManager : MonoBehaviour
{
    public static int MINUTES_ON_CLOCK = 60;
    public static int HOURS_ON_CLOCK = 12;

    public Transform minutesHand;
    public Transform hoursHand;

    public int rightAttempts = 0;
    public float timerVal = 60;
    private bool checkOneTime = true;

    private MyTime timeToGet, currentTime;
    private ClockUIManager uIManager;

    void Start()
    {
        uIManager = FindObjectOfType<ClockUIManager>();
        currentTime = new MyTime().SetHour(GetClockByAngle(System.Math.Abs(hoursHand.rotation.eulerAngles.z - 360), HOURS_ON_CLOCK))
            .SetMinute(GetClockByAngle(System.Math.Abs(minutesHand.rotation.eulerAngles.z - 360), MINUTES_ON_CLOCK));
        GenerateNewTask();
    }

    public void GenerateNewTask()
    {
        timeToGet = GenerateTime();
        uIManager.SetTimeText(timeToGet);
        uIManager.SetNextButtonActive(false);
        checkOneTime = true;
    }

    void Update()
    {
        timerVal -= Time.deltaTime;

        currentTime.SetHour(GetClockByAngle(System.Math.Abs(hoursHand.rotation.eulerAngles.z - 360), HOURS_ON_CLOCK))
            .SetMinute(GetClockByAngle(System.Math.Abs(minutesHand.rotation.eulerAngles.z - 360), MINUTES_ON_CLOCK));

        print(currentTime);

        if (currentTime.Hour == timeToGet.Hour && currentTime.Minute == timeToGet.Minute &&checkOneTime)
        {
            uIManager.SetNextButtonActive(true);
            checkOneTime = false;
            rightAttempts++;
        }

        if (timerVal <= 0)
        {
            uIManager.GameOver();
        }
    }

    private MyTime GenerateTime()
    {
        return new MyTime().SetHour(Random.Range(0, HOURS_ON_CLOCK + 1)).SetMinute(Random.Range(0, MINUTES_ON_CLOCK));
    }

    public int GetClockByAngle(float angle, int bas)
    {
        return Mathf.FloorToInt(GetPercent(angle, 360) * bas);
    }

    public float GetPercent(float amount, float from)
    {
        return amount / from;
    }
}