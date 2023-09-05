using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TFPlay.BuildIncrementor;
using TFPlay.BuildTools.AppPackageExtractor;

namespace TFPlay.BuildTools.Builder
{
    public class BuildHelperEditorWindow : EditorWindow
    {
        private const string CustomMenuBasePath = "Build Helper/Open";

        [MenuItem(CustomMenuBasePath)]
        private static void Open()
        {
            var window = GetWindow<BuildHelperEditorWindow>("Build Helper");
            window.minSize = new Vector2(400, 400);
            window.Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Open Build Version Settings"))
            {
                BuildVersionSettingsEditorWindow.Open(BuildVersionSettings.Instance);
            }

            GUILayout.Space(8);
            GUILayout.BeginVertical("box");
            BuildHelper.DevelopmentBuildEnabled = GUILayout.Toggle(BuildHelper.DevelopmentBuildEnabled, "Development Build");
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Scripting Beckend:", GUILayout.Width(150));
            BuildHelper.ScriptingBeckend = EditorGUILayout.Popup(BuildHelper.ScriptingBeckend, new string[] { "Mono", "IL2CPP" });
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Compression Method:", GUILayout.Width(150));
            BuildHelper.CompressionMethod = EditorGUILayout.Popup(BuildHelper.CompressionMethod, new string[] { "Default", "LZ4", "LZ4HC" });
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.Space(8);
            GUILayout.BeginVertical("box");
#if UNITY_IOS
            if (GUILayout.Button("Build iOS Xcode Project"))
            {
                BuildHelper.BuildIOSXcodeProject();
                GUIUtility.ExitGUI();
            }
#endif
#if UNITY_ANDROID
            if (GUILayout.Button("Build (apk)"))
            {
                BuildHelper.BuildAndroidAppPackage();
                GUIUtility.ExitGUI();
            }
            if (GUILayout.Button("Build and Run (apk)"))
            {
                BuildHelper.BuildAndRunAndroidAppPackage();
                GUIUtility.ExitGUI();
            }
            if (GUILayout.Button("Build (aab)"))
            {
                BuildHelper.BuildAndroidAppBundle();
                GUIUtility.ExitGUI();
            }
#endif
            GUILayout.Space(8);
            if (GUILayout.Button("Open Builds Folder"))
            {
                BuildHelper.OpenBuildsFolder();
                GUIUtility.ExitGUI();
            }
            GUILayout.EndVertical();
#if UNITY_ANDROID
            GUILayout.Space(8);
            GUILayout.BeginVertical("box");
            ApplicationPackageExtractor.ExtractAfterBuildEnabled = GUILayout.Toggle(ApplicationPackageExtractor.ExtractAfterBuildEnabled, "Extract After Build");
            if (GUILayout.Button("Extract apk from aab"))
            {
                ApplicationPackageExtractor.ExtractAppPackage();
                GUIUtility.ExitGUI();
            }
            GUILayout.EndVertical();
#endif
        }
    }
}
