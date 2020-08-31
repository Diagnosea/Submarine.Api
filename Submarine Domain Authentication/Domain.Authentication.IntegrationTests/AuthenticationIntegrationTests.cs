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
            if (_mongoDbRunner == null)
            {
                _mongoDbRunner = MongoDbRunner.Start();
            }

            if (Database == null)
            {
                var client = new MongoClient(_mongoDbRunner.ConnectionString);
                Database = client.GetDatabase(nameof(AuthenticationIntegrationTests));
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (!_mongoDbRunner.Disposed)
            {
                _mongoDbRunner.Dispose();
            };
        }
    }
}