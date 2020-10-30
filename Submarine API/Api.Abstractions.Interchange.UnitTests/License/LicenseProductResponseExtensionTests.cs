using System;
using Diagnosea.Submarine.Api.Abstractions.Interchange.License;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.TestPack;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.License
{
    [TestFixture]
    public class LicenseProductResponseExtensionTests
    {
        public class ToResponse : LicenseProductResponseExtensionTests
        {
            [Test]
            public void GivenLicenseProductDto_ReturnsLicenseProductResponse()
            {
                // Arrange
                var licenseProduct = new LicenseProductDto
                {
                    Name = "Product Name",
                    Expiration = DateTime.UtcNow
                };
                
                // Act
                var result = licenseProduct.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Name, Is.EqualTo(licenseProduct.Name));
                    DiagnoseaAssert.That(result.Expiration, Is.EqualTo(licenseProduct.Expiration));
                });
            }
        }
    }
}