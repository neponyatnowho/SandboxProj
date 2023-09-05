using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace TFPlay.BuildTools.AppPackageExtractor
{
    public class ExtractAndroidAppBundle : IPostprocessBuildWithReport
    {
        public int callbackOrder => 999;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (!ApplicationPackageExtractor.ExtractAfterBuildEnabled)
                return;

            if (report.summary.platform == BuildTarget.Android && EditorUserBuildSettings.buildAppBundle)
            {
                var aabFilePath = report.summary.outputPath;
                ApplicationPackageExtractor.ExtractAppPackage(aabFilePath);
            }
        }
    }
}
