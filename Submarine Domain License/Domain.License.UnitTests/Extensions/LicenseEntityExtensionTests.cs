using System;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Extensions;
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
                    UserId = Guid.NewGuid()
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
                    UserId = Guid.NewGuid()
                };
                
                // Act
                var result = command.ToEntity();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(command.Id));
                    Assert.That(result.UserId, Is.EqualTo(command.UserId));
                });
            }
        }
    }
}