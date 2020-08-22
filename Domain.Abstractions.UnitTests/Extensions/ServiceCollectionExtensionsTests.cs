using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NUnit.Framework;
using Submarine.Domain.Abstractions.Extensions;

namespace Submarine.Domain.Abstractions.UnitTests.Extensions
{
    [TestFixture]
    public class ServiceCollectionExtensionsTests
    {
        public class AddSubmarineDatabase : ServiceCollectionExtensionsTests
        {
            [Test]
            public void GivenDatabaseSetup_AddsIMongoDatabaseToServiceCollection()
            {
                // Arrange
                const string connectionString = "mongodb://thisisnotaurl.com/mydb";
                var serviceCollection = new ServiceCollection();
                
                // Act
                serviceCollection.AddSubmarineDatabase(builder => builder.WithConnectionString(connectionString));
                
                // Assert
                var provider = serviceCollection.BuildServiceProvider();
                var service = provider.GetService<IMongoDatabase>();
                Assert.That(service, Is.Not.Null);
            }
        }
    }
}