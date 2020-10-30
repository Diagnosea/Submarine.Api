using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseById;
using Diagnosea.TestPack;
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
                    Id = licenseId,
                    Key = "This is a key",
                    Created = DateTime.UtcNow,
                    UserId = Guid.NewGuid(),
                    Products = new List<LicenseProductEntity>
                    {
                        new LicenseProductEntity
                        {
                            Name = "Product Name",
                            Key = "This is a key",
                            Created = DateTime.UtcNow,
                            Expiration = DateTime.UtcNow.AddDays(1)
                        }
                    }
                };

                await _licenseCollection.InsertOneAsync(license, null, CancellationToken.None);
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(licenseId));
                    Assert.That(result.Key, Is.Not.EqualTo(license.Key));
                    DiagnoseaAssert.That(result.Created, Is.Not.EqualTo(license.Created));
                    Assert.That(result.UserId, Is.Not.Null);
                    CollectionAssert.IsNotEmpty(result.Products);
                    Assert.That(result.Products[0].Name, Is.EqualTo(license.Products[0].Name));
                    Assert.That(result.Products[0].Key, Is.Not.EqualTo(license.Products[0].Key));
                    DiagnoseaAssert.That(result.Products[0].Created, Is.Not.EqualTo(license.Products[0].Created));
                    DiagnoseaAssert.That(result.Products[0].Expiration, Is.EqualTo(license.Products[0].Expiration));
                });
            }
        }
    }
}