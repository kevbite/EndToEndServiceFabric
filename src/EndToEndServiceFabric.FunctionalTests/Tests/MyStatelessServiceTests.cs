using System;
using System.Net.Http;
using NUnit.Framework;

namespace EndToEndServiceFabric.FunctionalTests.Tests
{
    [TestFixture]
    public class MyStatelessServiceTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8661")
            };
        }

        [Test]
        public void ShouldReturnTheMultiplicationValue()
        {
            Assert.That(async () =>
            {
                var response = await _httpClient.GetAsync("/Multiplication?a=10&b=5")
                    .ConfigureAwait(false);

                var value = await response.Content.ReadAsAsync<int>().ConfigureAwait(false);

                return value;
            }, Is.EqualTo(50).After(30000, 1000));
        }
    }
}
