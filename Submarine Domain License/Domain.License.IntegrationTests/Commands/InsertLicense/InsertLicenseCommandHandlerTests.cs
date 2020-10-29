using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Entities;
using MongoDB.Driver;
using NUnit.Framework;

namespace Submarine.Domain.License.IntegrationTests.Commands.InsertLicense
{
    [TestFixture]
    public class InsertLicenseCommandHandlerTests : MongoIntegrationTests
    {
        private IMongoCollection<LicenseEntity> _licenseCollection;
        private InsertLicenseCommandHandler _classUnderTest;
        

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _licenseCollection = MongoDatabase.GetCollection<LicenseEntity>("License");
            _classUnderTest = new InsertLicenseCommandHandler(MongoDatabase);
        }

        [TearDown]
        public void TearDown()
        {
            _licenseCollection.DeleteMany(FilterDefinition<LicenseEntity>.Empty);
        }
        
        public class Handle : InsertLicenseCommandHandlerTests
        {
            [Test]
            public async Task GivenLicense_InsertsLicenseIntoDatabase()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var command = new InsertLicenseCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };
                
                // Act
                var result = await _classUnderTest.Handle(command, cancellationToken);
                
                // Assert
                var entity = await _licenseCollection
                    .Find(x => x.Id == command.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(entity.UserId, Is.EqualTo(command.UserId));
                });
            }
        }
    }
}