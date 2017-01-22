using System;
using System.Fabric;
using System.Fabric.Health;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EndToEndServiceFabric.FunctionalTests.Tests
{
    [TestFixture]
    public class ApplicationHealthTests
    {
        private FabricClient _fabricClient;

        [SetUp]
        public void SetUp()
        {
            _fabricClient = new FabricClient();
        }

        [Test]
        public async Task ShouldHaveAnOkAggregatedHealthState()
        {
            var applicationHealth = await GetApplicationHealth()
                                            .ConfigureAwait(false);

            Assert.That(applicationHealth.AggregatedHealthState, Is.EqualTo(HealthState.Ok));
        }

        [Test]
        public void ShouldHaveHealthyMyStatelessService()
        {
            var uriBuilder = new UriBuilder(Initialize.ApplicationUri);
            uriBuilder.Path += "/MyStatelessService";

            Assert.That(async () => await GetServiceHealthState(uriBuilder.Uri).ConfigureAwait(false), Is.EqualTo(HealthState.Ok).After(30000, 200));
        }
        
        private async Task<ApplicationHealth> GetApplicationHealth()
        {
            var applicationHealth =
                await _fabricClient.HealthManager.GetApplicationHealthAsync(Initialize.ApplicationUri)
                .ConfigureAwait(false);

            return applicationHealth;
        }

        private async Task<HealthState?> GetServiceHealthState(Uri serviceUri)
        {
            return (await GetApplicationHealth().ConfigureAwait(false)).ServiceHealthStates.FirstOrDefault(x => x.ServiceName == serviceUri)?.AggregatedHealthState;
        }
    }
}
