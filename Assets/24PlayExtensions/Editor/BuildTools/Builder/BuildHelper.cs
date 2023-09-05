using UnityEditor;
using System.IO;
using UnityEngine;

namespace TFPlay.BuildTools.Builder
{
    public class BuildHelper
    {
        private const string DevelopmentBuildPrefKey = "DevelopmentBuildSettings";
        private const string ScriptingBeckendPrefKey = "ScriptingBeckendSettings";
        private const string CompressionMethodPrefKey = "CompressionMethodSettings";

        public static bool DevelopmentBuildEnabled
        {
            get => EditorPrefs.GetBool(DevelopmentBuildPrefKey, false);
            set => EditorPrefs.SetBool(DevelopmentBuildPrefKey, value);
        }

        public static int ScriptingBeckend
        {
            get => EditorPrefs.GetInt(ScriptingBeckendPrefKey, 0);
            set => EditorPrefs.SetInt(ScriptingBeckendPrefKey, value);
        }

        public static int CompressionMethod
        {
            get => EditorPrefs.GetInt(CompressionMethodPrefKey, 1);
            set => EditorPrefs.SetInt(CompressionMethodPrefKey, value);
        }

        private static BuildOptions GetCompressionMethod()
        {
            switch (CompressionMethod)
            {
                case 1: return BuildOptions.CompressWithLz4;
                case 2: return BuildOptions.CompressWithLz4HC;
            }
            return BuildOptions.None;
        }

#if UNITY_ANDROID
        private static ScriptingImplementation GetScriptingImplementation()
        {
            return ScriptingBeckend == 0 ? ScriptingImplementation.Mono2x : ScriptingImplementation.IL2CPP;
        }

        public static void BuildAndroidAppPackage()
        {
            ProjectSettingsHelper.FixSettings();
            var androidBuildConfig = new AndroidBuildConfig(DevelopmentBuildEnabled, false, false, GetScriptingImplementation(), GetCompressionMethod());
            var androidBuilder = new AndroidBuilder(androidBuildConfig);
            androidBuilder.Build();
        }

        public static void BuildAndRunAndroidAppPackage()
        {
            ProjectSettingsHelper.FixSettings();
            var androidBuildConfig = new AndroidBuildConfig(DevelopmentBuildEnabled, true, false, GetScriptingImplementation(), GetCompressionMethod());
            var androidBuilder = new AndroidBuilder(androidBuildConfig);
            androidBuilder.Build();
        }

        public static void BuildAndroidAppBundle()
        {
            ProjectSettingsHelper.FixSettings();
            var androidBuildConfig = new AndroidBuildConfig(DevelopmentBuildEnabled, false, true, ScriptingImplementation.IL2CPP, GetCompressionMethod());
            var androidBuilder = new AndroidBuilder(androidBuildConfig);
            androidBuilder.Build();
        }
#endif

#if UNITY_IOS
        public static void BuildIOSXcodeProject()
        {
            ProjectSettingsHelper.FixSettings();
            var iosBuildConfig = new iOSBuildConfig(DevelopmentBuildEnabled, GetCompressionMethod());
            var iosBuilder = new iOSBuilder(iosBuildConfig);
            iosBuilder.Build();
        }
#endif

        public static void OpenBuildsFolder()
        {
            var directoryPath = Path.GetDirectoryName(Application.dataPath);
            var buildsPath = Path.Combine(directoryPath, "Builds");
            Application.OpenURL($"file://{buildsPath}");
        }
    }
}
