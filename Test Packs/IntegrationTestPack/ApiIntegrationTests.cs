using System.Net.Http;
using Diagnosea.IntegrationTestPack.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.IntegrationTestPack
{
    public class WebApiIntegrationTests<TStartup> where TStartup : class
    {
        protected HttpClient HttpClient { get; set; }
        protected IMongoDatabase MongoDatabase { get; set; }

        private MongoDbRunner _mongoDbRunner;
        private SubmarineWebApplicationFactory<TStartup> _webApplicationFactory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            
            var client = new MongoClient(_mongoDbRunner.ConnectionString);
            MongoDatabase = client.GetDatabase(nameof(WebApiIntegrationTests<TStartup>));

            var webApplicationFactoryOptions = new SubmarineWebApplicationFactoryOptions {Database = MongoDatabase};
            _webApplicationFactory = new SubmarineWebApplicationFactory<TStartup>(webApplicationFactoryOptions);

            HttpClient = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mongoDbRunner.Dispose();
            _webApplicationFactory.Dispose();
        }

        protected virtual string GetUrl(params string[] parts) => string.Join("/", parts);
    }
}