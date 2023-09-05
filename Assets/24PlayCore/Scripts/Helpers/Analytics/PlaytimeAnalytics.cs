using GameAnalyticsSDK;
using System.Collections;
using UnityEngine;

namespace TFPlay.Analytics
{
    public class PlaytimeAnalytics : MonoBehaviour
    {
        private const string PlaytimeKey = "Playtime";
        private const int SecondsInMinute = 60;
        private const int BenchmarkTimeInSeconds = 10 * SecondsInMinute;

        public static void Initialize()
        {
            var playtimeInSeconds = GetPlaytime();
            if (playtimeInSeconds <= BenchmarkTimeInSeconds)
            {
                var go = new GameObject(nameof(PlaytimeAnalytics));
                go.AddComponent<PlaytimeAnalytics>();
                DontDestroyOnLoad(go);
            }
        }

        private static int GetPlaytime()
        {
            var seconds = PlayerPrefs.GetInt(PlaytimeKey, 0);
            return seconds;
        }

        private static void SavePlaytime(int newTime)
        {
            PlayerPrefs.SetInt(PlaytimeKey, newTime);
            PlayerPrefs.Save();
        }

        private IEnumerator Start()
        {
            var playtimeInSeconds = GetPlaytime();
            var delay = new WaitForSecondsRealtime(1f);
            while (playtimeInSeconds <= BenchmarkTimeInSeconds)
            {
                yield return delay;
                playtimeInSeconds++;
                SavePlaytime(playtimeInSeconds);
                if (playtimeInSeconds % SecondsInMinute == 0)
                {
                    var minute = playtimeInSeconds / SecondsInMinute;
                    GameAnalytics.NewDesignEvent("Playtime:" + minute);
                }
            }
        }
    }
}
