using UnityEditor;

namespace TFPlay.BuildTools.Builder
{
    public abstract class BaseBuildConfig
    {
        public bool developmentBuild;
        public BuildOptions buildOptions;

        protected BaseBuildConfig(bool developmentBuild, BuildOptions buildOptions)
        {
            this.developmentBuild = developmentBuild;
            this.buildOptions = buildOptions;
        }
    }
}

