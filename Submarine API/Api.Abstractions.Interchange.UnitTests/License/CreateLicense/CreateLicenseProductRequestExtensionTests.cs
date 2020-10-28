using System;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.License.CreateLicense
{
    [TestFixture]
    public class CreateLicenseProductRequestExtensionTests
    {
        public class ToDto : CreateLicenseProductRequestExtensionTests
        {
            [Test]
            public void GivenNoExpirationValue_ThrowsSubmarineMappingException()
            {
                // Arrange
                var request = new CreateLicenseProductRequest();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.Throws<SubmarineMappingException>(() => request.ToDto());
                    
                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.MappingException));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(MappingExceptionMessages.Failed));
                });
            }

            [Test]
            public void GivenValidRequest_ReturnsDto()
            {
                // Arrange
                var request = new CreateLicenseProductRequest
                {
                    Name = "This is a name",
                    Expiration = DateTime.UtcNow
                };
                
                // Act
                var result = request.ToDto();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Name, Is.EqualTo(request.Name));
                    Assert.That(result.Expiration, Is.EqualTo(request.Expiration));
                });
            }
        }
    }
}