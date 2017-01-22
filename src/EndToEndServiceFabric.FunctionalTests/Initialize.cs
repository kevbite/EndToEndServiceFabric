using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EndToEndServiceFabric.FunctionalTests
{
    [SetUpFixture]
    public class Initialize
    {
        private FabricApplicationDeployer _deployer;

        public static Uri ApplicationUri { get; private set; }

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var pathFinder = new PathFinder();
            var paths = pathFinder.Find("EndToEndServiceFabric");

            var applicationType = "EndToEndServiceFabricType";
            var applicationTypeVersion = "1.0.0";

            var msBuildPackager = new MsBuildPacker();
            msBuildPackager.Pack(paths.SfProj);

            _deployer = new FabricApplicationDeployer(paths.SfPackagePath, applicationType, applicationTypeVersion);
            ApplicationUri = await _deployer.DeployAsync().ConfigureAwait(false);

        }

        [OneTimeTearDown]
        public async Task Kill()
        {
            await _deployer.RemoveAsync()
                .ConfigureAwait(false);
        }
    }
}