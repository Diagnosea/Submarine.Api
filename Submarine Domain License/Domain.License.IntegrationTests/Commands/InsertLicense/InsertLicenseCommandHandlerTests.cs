using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.TestPack;
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
                    UserId = Guid.NewGuid(),
                    Products = new List<InsertLicenseProductCommand>
                    {
                        new InsertLicenseProductCommand
                        {
                            Name = "This is a product",
                            Key = "This is a product key",
                            Expiration = DateTime.UtcNow.AddDays(1)
                        }
                    }
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
                    Assert.That(entity.Products, Is.Not.Null);

                    var product = command.Products.FirstOrDefault();
                    var stub = entity.Products.FirstOrDefault();

                    Assert.That(stub, Is.Not.Null);
                    Assert.That(stub.Name, Is.EqualTo(product.Name));
                    Assert.That(stub.Key, Is.EqualTo(product.Key));
                    DiagnoseaAssert.That(stub.Expiration, Is.EqualTo(product.Expiration));
                });
            }
        }
    }
}