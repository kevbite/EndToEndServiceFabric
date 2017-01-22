using System;
using System.Collections.Specialized;
using System.Fabric;
using System.Fabric.Description;
using System.Threading.Tasks;

namespace EndToEndServiceFabric.FunctionalTests
{
    public class FabricApplicationDeployer : IDisposable
    {
        private readonly string _packagePath;
        private readonly string _applicationType;
        private readonly string _applicationPathVersion;
        private readonly FabricClient _client;
        private string _imageStorePath;
        private readonly string _imageStoreConnectionString;
        private Uri _applicationName;

        public FabricApplicationDeployer(string packagePath, string applicationType, string applicationPathVersion)
        {
            this._packagePath = packagePath;
            this._applicationType = applicationType;
            this._applicationPathVersion = applicationPathVersion;
            _client = new FabricClient();
            _imageStoreConnectionString = @"file:C:\SfDevCluster\Data\ImageStoreShare";
        }

        public async Task<Uri> DeployAsync()
        {
            _imageStorePath = $"{Guid.NewGuid()}";
            _applicationName = new Uri($"fabric:/{Guid.NewGuid()}");

            _client.ApplicationManager.CopyApplicationPackage(@"file:C:\SfDevCluster\Data\ImageStoreShare",
                _packagePath,
                _imageStorePath);

            await _client.ApplicationManager.ProvisionApplicationAsync(_imageStorePath)
                .ConfigureAwait(false);

            await _client.ApplicationManager.CreateApplicationAsync(
                new ApplicationDescription(_applicationName,
                    _applicationType,
                    _applicationPathVersion,
                    new NameValueCollection()
                    {

                    })
            ).ConfigureAwait(false);

            return _applicationName;
        }

        public async Task RemoveAsync()
        {
            await _client.ApplicationManager.DeleteApplicationAsync(new DeleteApplicationDescription(_applicationName))
                .ConfigureAwait(false);

            await _client.ApplicationManager.UnprovisionApplicationAsync(_applicationType, _applicationPathVersion)
                .ConfigureAwait(false);

            _client.ApplicationManager.RemoveApplicationPackage(_imageStoreConnectionString, _imageStorePath);
        }

        public void Dispose()
        {
            RemoveAsync().GetAwaiter().GetResult();
        }
    }
}