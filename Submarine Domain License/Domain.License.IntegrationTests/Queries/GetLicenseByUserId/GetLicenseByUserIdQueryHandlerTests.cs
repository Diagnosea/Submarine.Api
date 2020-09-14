using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId;
using Diagnosea.Submarine.TestingUtilities;
using MongoDB.Driver;
using NUnit.Framework;

namespace Submarine.Domain.License.IntegrationTests.Queries.GetLicenseByUserId
{
    [TestFixture]
    public class GetLicenseByUserIdQueryHandlerTests : MongoIntegrationTests
    {
        private IMongoCollection<LicenseEntity> _licenseCollection;
        private GetLicenseByUserIdQueryHandler _classUnderTest;

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _licenseCollection = MongoDatabase.GetEntityCollection<LicenseEntity>();
            _classUnderTest = new GetLicenseByUserIdQueryHandler(MongoDatabase);
        }

        [TearDown]
        public void TearDown()
        {
            _licenseCollection.DeleteMany(FilterDefinition<LicenseEntity>.Empty);
        }

        public class Handle : GetLicenseByUserIdQueryHandlerTests
        {
            [Test]
            public async Task GivenLicenseWithoutUserId_ReturnsNull()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var query = new GetLicenseByUserIdQuery
                {
                    UserId = Guid.NewGuid()
                };

                var license = new LicenseEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };

                await _licenseCollection.InsertOneAsync(license, null, cancellationToken);
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.That(result, Is.Null);
            }

            [Test]
            public async Task GivenLicenseWithUserId_ReturnsLicense()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var userId = Guid.NewGuid();

                var query = new GetLicenseByUserIdQuery {UserId = userId};
                
                var license = new LicenseEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };

                await _licenseCollection.InsertOneAsync(license, null, cancellationToken);
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.Not.Null);
                    Assert.That(result.UserId, Is.EqualTo(userId));
                    CollectionAssert.IsEmpty(result.Products);
                });
            }
        }
    }
}