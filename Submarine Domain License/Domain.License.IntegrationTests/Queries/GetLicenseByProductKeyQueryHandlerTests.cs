using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByProductKey;
using MongoDB.Driver;
using NUnit.Framework;

namespace Submarine.Domain.License.IntegrationTests.Queries
{
    [TestFixture]
    public class GetLicenseByProductKeyQueryHandlerTests : MongoIntegrationTests
    {
        private IMongoCollection<LicenseEntity> _licenseCollection;
        private GetLicenseByProductKeyQueryHandler _classUnderTest;

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _licenseCollection = MongoDatabase.GetCollection<LicenseEntity>("License");
            _classUnderTest = new GetLicenseByProductKeyQueryHandler(MongoDatabase);
        }

        [TearDown]
        public void TearDown()
        {
            _licenseCollection.DeleteMany(FilterDefinition<LicenseEntity>.Empty);
        }

        public class Handle : GetLicenseByProductKeyQueryHandlerTests
        {
            [Test]
            public async Task GivenLicenseWithoutProductKey_ReturnsNull()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var query = new GetLicenseByProductKeyQuery
                {
                    ProductKey = "This is the product key being looked for"
                };
                
                var product = new LicenseProductEntity
                {
                    Name = "Product With Invalid Key",
                    Key = "This is not the key being looked for",
                    Expiration = DateTime.UtcNow.AddDays(1)
                };

                var license = new LicenseEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Products = new List<LicenseProductEntity> {product}
                };

                await _licenseCollection.InsertOneAsync(license, null, cancellationToken);
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.That(result, Is.Null);
            }

            [Test]
            public async Task GivenLicenseWithProductKey_ReturnsLicense()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                const string productKey = "This is the product key being looked for";

                var query = new GetLicenseByProductKeyQuery {ProductKey = productKey};

                var product = new LicenseProductEntity
                {
                    Name = "Product With Invalid Key",
                    Key = productKey,
                    Expiration = DateTime.UtcNow.AddDays(1)
                };

                var license = new LicenseEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Products = new List<LicenseProductEntity> {product}
                };

                await _licenseCollection.InsertOneAsync(license, null, cancellationToken);
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.Not.Null);
                    Assert.That(result.UserId, Is.EqualTo(license.UserId));
                    CollectionAssert.IsEmpty(result.Products);
                });
            }
        }
    }
}