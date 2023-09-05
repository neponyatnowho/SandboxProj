using GameAnalyticsSDK;
using TFPlay.Analytics;
using UnityEngine;

public static class AnalyticsHelper
{
    private static bool levelWasStarted;

    public static void Init()
    {
        GameAnalytics.Initialize();
        FacebookHelper.Initialize();
        PlaytimeAnalytics.Initialize();
        LogMessage("Analytics - Init");
    }

    public static void StartLevel()
    {
        levelWasStarted = true;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GetCurrentLevelName());
        LogMessage("Analytics - StartLevel:" + GetCurrentLevelIndex());
    }

    public static void CompleteLevel()
    {
        if (!levelWasStarted)
            return;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, GetCurrentLevelName());
        LogMessage("Analytics - CompleteLevel:" + GetCurrentLevelIndex());
        IncrementLevel();
        levelWasStarted = false;
    }

    public static void FailLevel()
    {
        if (!levelWasStarted)
            return;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, GetCurrentLevelName());
        LogMessage("Analytics - FailLevel:" + GetCurrentLevelIndex());
        levelWasStarted = false;
    }

    private static int GetCurrentLevelIndex()
    {
        return PlayerPrefs.GetInt("AH_GA_Level", 1);
    }

    private static string GetCurrentLevelName()
    {
        return string.Format("Level_{0}", GetCurrentLevelIndex());
    }

    private static void IncrementLevel()
    {
        int curLevel = GetCurrentLevelIndex();
        PlayerPrefs.SetInt("AH_GA_Level", curLevel + 1);
        PlayerPrefs.Save();
    }

    private static void LogMessage(string message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=white>{message}</color>");
#else
        Debug.Log(message);
#endif
    }
}