using System.Reflection;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.IntegrationTestPack
{
    public class MongoIntegrationTests
    {
        private MongoDbRunner _mongoDbRunner;
        protected IMongoDatabase MongoDatabase;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            if (_mongoDbRunner == null)
            {
                _mongoDbRunner = MongoDbRunner.Start();
            }

            if (MongoDatabase == null)
            {
                var client = new MongoClient(_mongoDbRunner.ConnectionString);
                MongoDatabase = client.GetDatabase(Assembly.GetExecutingAssembly().GetName().Name);
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