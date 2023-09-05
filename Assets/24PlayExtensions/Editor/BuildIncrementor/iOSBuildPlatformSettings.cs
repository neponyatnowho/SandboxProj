using System;
using UnityEditor;

namespace TFPlay.BuildIncrementor
{
    [Serializable]
    public class iOSBuildPlatformSettings : BuildPlatformSettings
    {
        public override int MajorVersion
        {
            get { return base.MajorVersion; }
            set
            {
                BuildNumber = 1;
                base.MajorVersion = value;
            }
        }

        public override int MinorVersion
        {
            get { return base.MinorVersion; }
            set
            {
                BuildNumber = 1;
                base.MinorVersion = value;
            }
        }

        public override int PatchVersion
        {
            get { return base.PatchVersion; }
            set
            {
                BuildNumber = 1;
                base.PatchVersion = value;
            }
        }

        public override void UpdateBuildVersion()
        {
            base.UpdateBuildVersion();
#if UNITY_IOS
            PlayerSettings.bundleVersion = BuildVersion;
            PlayerSettings.iOS.buildNumber = BuildNumber.ToString();
#endif
        }
    }
}
