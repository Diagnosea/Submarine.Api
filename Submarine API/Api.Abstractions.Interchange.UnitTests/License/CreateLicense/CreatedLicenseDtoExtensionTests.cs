using System;
using Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.License.CreateLicense
{
    [TestFixture]
    public class CreatedLicenseDtoExtensionTests
    {
        public class ToResponse : CreatedLicenseDtoExtensionTests
        {
            [Test]
            public void GivenValidDto_ReturnsResponse()
            {
                // Arrange
                var dto = new CreatedLicenseDto
                {
                    LicenseId = Guid.NewGuid()
                };
                
                // Act
                var result = dto.ToResponse();
                
                // Assert
                Assert.That(result.LicenseId, Is.EqualTo(dto.LicenseId));
            }
        }
    }
}