using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.Authentication.IntegrationTests
{
    public class AuthenticationIntegrationTests
    {
        private MongoDbRunner _mongoDbRunner;
        protected IMongoDatabase Database;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mongoDbRunner ??= MongoDbRunner.Start();

            if (Database != null) return;
            
            var client = new MongoClient(_mongoDbRunner.ConnectionString);
            Database = client.GetDatabase(nameof(AuthenticationIntegrationTests));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mongoDbRunner.Dispose();
        }
    }
}