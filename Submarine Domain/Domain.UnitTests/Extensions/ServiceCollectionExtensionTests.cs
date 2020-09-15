using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Domain.Instructors.UnitTests.Extensions
{
    [TestFixture]
    public class ServiceCollectionExtensionTests
    {
        public class AddMediatorDatabase : ServiceCollectionExtensionTests
        {
            [Test]
            public void AddsDatabaseToCollection()
            {
                // Arrange
                var services = new ServiceCollection();

                const string testConnectionString = "mongodb://test-connection.com/testDatabase";
                
                // Arrange
                services.AddSubmarineDatabase(builder => builder.WithConnectionString(testConnectionString));
                
                // Arrange
                var provider = services.BuildServiceProvider();
                var service = provider.GetRequiredService<IMongoDatabase>();

                Assert.That(service, Is.Not.Null);
                Assert.That(service.DatabaseNamespace.DatabaseName, Is.EqualTo("testDatabase"));
            }
        }
    }
}