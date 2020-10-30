using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Api.Abstractions.Interchange.License;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.TestPack;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.License
{
    [TestFixture]
    public class LicenseResponseExtensionTests
    {
        public class ToResponse : LicenseProductResponseExtensionTests
        {
            [Test]
            public void GivenLicenseDto_ReturnsLicenseProduct()
            {
                // Arrange
                var license = new LicenseDto
                {
                    Id = Guid.NewGuid(),
                    Products = new List<LicenseProductDto>
                    {
                        new LicenseProductDto
                        {
                            Name = "Product Name",
                            Expiration = DateTime.UtcNow
                        }
                    }
                };
                
                // Act
                var result = license.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(license.Id));
                    Assert.That(result.Products[0].Name, Is.EqualTo(result.Products[0].Name));
                    DiagnoseaAssert.That(result.Products[0].Expiration, Is.EqualTo(result.Products[0].Expiration));
                });
            }
        }
    }
}