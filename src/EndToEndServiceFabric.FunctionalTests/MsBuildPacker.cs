using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace EndToEndServiceFabric.FunctionalTests
{
    public class MsBuildPacker
    {
        public void Pack(string projPath)
        {
            var msbuildPath = GetMsBuildToolPath();
            var process = Process.Start($@"{msbuildPath}\msbuild.exe",
                $@" ""{projPath}"" /t:Package /p:Configuration={Configuration.Current},Platform=x64");

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception("msbuild package failed");
            }
        }

        private string GetMsBuildToolPath()
        {
            return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSBuild\ToolsVersions\4.0",
                "MSBuildToolsPath", null);
        }
    }
}