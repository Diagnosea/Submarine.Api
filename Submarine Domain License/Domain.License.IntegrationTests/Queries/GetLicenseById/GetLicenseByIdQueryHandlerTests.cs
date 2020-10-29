using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseById;
using MongoDB.Driver;
using NUnit.Framework;

namespace Submarine.Domain.License.IntegrationTests.Queries.GetLicenseById
{
    [TestFixture]
    public class GetLicenseByIdQueryHandlerTests : MongoIntegrationTests
    {
        private IMongoCollection<LicenseEntity> _licenseCollection;
        private GetLicenseByIdQueryHandler _classUnderTest;

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _licenseCollection = MongoDatabase.GetCollection<LicenseEntity>("License");
            _classUnderTest = new GetLicenseByIdQueryHandler(MongoDatabase);
        }
        
        [TearDown]
        public void TearDown()
        {
            _licenseCollection.DeleteMany(FilterDefinition<LicenseEntity>.Empty);
        }

        public class Handle : GetLicenseByIdQueryHandlerTests
        {
            [Test]
            public async Task GivenLicenseWithoutId_ReturnsNull()
            {
                // Arrange
                var query = new GetLicenseByIdQuery
                {
                    LicenseId = Guid.NewGuid()
                };

                var license = new LicenseEntity
                {
                    Id = Guid.NewGuid()
                };

                await _licenseCollection.InsertOneAsync(license, null, CancellationToken.None);
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                Assert.That(result, Is.Null);
            }

            [Test]
            public async Task GivenLicenseWithId_ReturnsLicense()
            {
                // Arrange
                var licenseId = Guid.NewGuid();
                
                var query = new GetLicenseByIdQuery
                {
                    LicenseId = licenseId
                };

                var license = new LicenseEntity
                {
                    Id = licenseId
                };

                await _licenseCollection.InsertOneAsync(license, null, CancellationToken.None);
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(licenseId));
                    Assert.That(result.UserId, Is.Not.Null);
                    CollectionAssert.IsEmpty(result.Products);
                });
            }
        }
    }
}