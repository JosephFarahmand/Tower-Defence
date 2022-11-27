using System;
using UnityEngine;

public static class TimeScaleHelper
{
    public static Action onChangeTimeScale;
    public static TimeScale CurrentTimeScale { get; private set; }

    public enum TimeScale
    {
        x0,
        x1,
        x2,
        x4,
        x10
    }
    public static void ChangeTimeScale(TimeScale timeScale)
    {
        switch (timeScale)
        {
            case TimeScale.x0:
                Time.timeScale = 0F;
                break;
            case TimeScale.x1:
                Time.timeScale = 1F;
                break;
            case TimeScale.x2:
                Time.timeScale = 2F;
                break;
            case TimeScale.x4:
                Time.timeScale = 4F;
                break;
            case TimeScale.x10:
                Time.timeScale = 10F;
                break;
        }

        CurrentTimeScale = timeScale;

        onChangeTimeScale?.Invoke();
    }

    public static void NextTimeScale()
    {
        switch (CurrentTimeScale)
        {
            case TimeScale.x0:
                ChangeTimeScale(TimeScale.x1);
                break;
            case TimeScale.x1:
                ChangeTimeScale(TimeScale.x2);
                break;
            case TimeScale.x2:
                ChangeTimeScale(TimeScale.x4);
                break;
            case TimeScale.x4:
                ChangeTimeScale(TimeScale.x10);
                break;
            case TimeScale.x10:
                ChangeTimeScale(TimeScale.x1);
                break;
        }
    }

    public static string GetTimeScaleName()
    {
        string name = "";
        switch (CurrentTimeScale)
        {
            case TimeScale.x0:
                name = "X0";
                break;
            case TimeScale.x1:
                name = "X1";
                break;
            case TimeScale.x2:
                name = "X2";
                break;
            case TimeScale.x4:
                name = "X4";
                break;
            case TimeScale.x10:
                name = "X10";
                break;
        }
        return name;
    }
}