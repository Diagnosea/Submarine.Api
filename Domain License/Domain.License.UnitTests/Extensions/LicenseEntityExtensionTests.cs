using System;
using System.Collections.Generic;
using System.Linq;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Extensions;
using Diagnosea.Submarine.TestingUtilities;
using NUnit.Framework;

namespace Submarine.Domain.License.UnitTests.Extensions
{
    [TestFixture]
    public class LicenseEntityExtensionTests
    {
        public class ToEntity : LicenseEntityExtensionTests
        {
            [Test]
            public void GivenInsertLicenseCommandWithoutProducts_ReturnsLicenseEntity()
            {
                // Arrange
                var command = new InsertLicenseCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Products = new List<InsertLicenseProductCommand>()
                };
                
                // Act
                var result = command.ToEntity();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(command.Id));
                    Assert.That(result.UserId, Is.EqualTo(command.UserId));
                    CollectionAssert.IsEmpty(result.Products);
                });
            }
            
            [Test]
            public void GivenInsertLicenseCommandWithProducts_ReturnsLicenseEntity()
            {
                // Arrange
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
                var result = command.ToEntity();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(command.Id));
                    Assert.That(result.UserId, Is.EqualTo(command.UserId));

                    var product = command.Products.FirstOrDefault();
                    var stub = result.Products.FirstOrDefault();

                    Assert.That(stub.Name, Is.EqualTo(product.Name));
                    Assert.That(stub.Key, Is.EqualTo(product.Key));
                    SubmarineAssert.That(stub.Expiration, Is.EqualTo(product.Expiration));
                });
            }
        }
    }
}