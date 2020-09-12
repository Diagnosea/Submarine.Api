using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using Utilities;

namespace Diagnosea.Submarine.Api.IntegrationTests
{
    public class ApiIntegrationTests
    {
        protected HttpClient Client { get; set; }
        protected IMongoDatabase Database { get; set; }

        private MongoDbRunner _mongoDbRunner;
        private SubmarineWebApplicationFactory<Startup> _webApplicationFactory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            
            var client = new MongoClient(_mongoDbRunner.ConnectionString);
            Database = client.GetDatabase(nameof(ApiIntegrationTests));

            var webApplicationFactoryOptions = new SubmarineWebApplicationFactoryOptions {Database = Database};
            _webApplicationFactory = new SubmarineWebApplicationFactory<Startup>(webApplicationFactoryOptions);

            Client = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
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
    }
}