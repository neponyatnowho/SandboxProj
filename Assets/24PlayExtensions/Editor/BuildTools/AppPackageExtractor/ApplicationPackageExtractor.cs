using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using AppPackageExtractor;

namespace TFPlay.BuildTools.AppPackageExtractor
{
    public class ApplicationPackageExtractor
    {
        private const string ExtractAfterBuildPrefKey = "ExtractAfterBuildSettings";

        private const string BundletoolFilePath = "Assets/24PlayExtensions/Editor/BuildTools/AppPackageExtractor/Plugins/bundletool-all-1.11.0.jar";
        private const string KeystorePassword = "242424";

        public static bool ExtractAfterBuildEnabled
        {
            get => EditorPrefs.GetBool(ExtractAfterBuildPrefKey, false);
            set => EditorPrefs.SetBool(ExtractAfterBuildPrefKey, value);
        }

        private static string CreateOrGetBuildDirectory()
        {
            var buildsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Builds");
            if (!Directory.Exists(buildsFolder))
            {
                Directory.CreateDirectory(buildsFolder);
            }
            return buildsFolder;
        }

        public static void ExtractAppPackage()
        {
            var aabFilePath = EditorUtility.OpenFilePanel("Choose abb file ...", CreateOrGetBuildDirectory(), "aab");
            if (!string.IsNullOrEmpty(aabFilePath))
            {
                ExtractAppPackage(aabFilePath);
            }
        }

        public static void ExtractAppPackage(string aabFilePath)
        {
            var keystoreFilePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), PlayerSettings.Android.keystoreName);
            var keystoreAlias = PlayerSettings.Android.keyaliasName;
            try
            {
                ApkExtractor.ExtractApkFileAsync(aabFilePath, keystoreFilePath, keystoreAlias, KeystorePassword, BundletoolFilePath);
                EditorUtility.RevealInFinder(Path.ChangeExtension(aabFilePath, ".apk"));
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Error while extracting apk ...", ex.Message, "OK");
            }
        }
    }
}
