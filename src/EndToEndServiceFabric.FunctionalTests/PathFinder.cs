using System;
using System.Reflection;

namespace EndToEndServiceFabric.FunctionalTests
{
    public class PathFinder
    {
        public ProjectPaths Find(string projectName)
        {
            var codeBase = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var slnRoot = codeBase.Substring(0, codeBase.IndexOf(@"\EndToEndServiceFabric.FunctionalTests", StringComparison.Ordinal));

            var applicationRoot = $@"{slnRoot}\{projectName}";

            var applicationSfProj = $@"{applicationRoot}\{projectName}.sfproj";
            var packagePath = $@"{applicationRoot}\pkg\{Configuration.Current}";

            return new ProjectPaths()
            {
                SfProj = applicationSfProj,
                SfPackagePath = packagePath
            };
        }
    }
}