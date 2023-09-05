using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class GAValicationProcess : DefaultValidationProcess
    {
        public override void Execute(BaseBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking GameAnalytics keys");
            var runtimePlatform = GetRuntimePlatform();
            if (!GameAnalytics.SettingsGA.Platforms.Contains(runtimePlatform))
            {
                OnValidationProcessFailed($"Add {runtimePlatform} platform in GA Settings");
            }
            for (int i = 0; i < GameAnalytics.SettingsGA.Platforms.Count; ++i)
            {
                if (GameAnalytics.SettingsGA.Platforms[i] == runtimePlatform)
                {
                    var gameKey = GameAnalytics.SettingsGA.GetGameKey(i);
                    if (string.IsNullOrEmpty(gameKey))
                    {
                        OnValidationProcessFailed($"Set Game Key for {runtimePlatform} platform in GA Settings");
                    }
                    GameAnalytics.SettingsGA.UpdateGameKey(i, gameKey.Trim());

                    var secretKey = GameAnalytics.SettingsGA.GetSecretKey(i);
                    if (string.IsNullOrEmpty(secretKey))
                    {
                        OnValidationProcessFailed($"Set Secret Key for {runtimePlatform} platform in GA Settings");
                    }
                    GameAnalytics.SettingsGA.UpdateSecretKey(i, secretKey.Trim());
                }
            }
        }

        private RuntimePlatform GetRuntimePlatform()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android: return RuntimePlatform.Android;
                case BuildTarget.iOS: return RuntimePlatform.IPhonePlayer;
                default: return RuntimePlatform.WindowsPlayer;
            }
        }
    }
}
