using System;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Diagnosea.Submarine.Api.Abstractions.Interchange.License.CreateLicense;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.License.CreateLicense
{
    [TestFixture]
    public class CreateLicenseRequestExtensionTests
    {
        public class ToDto : CreateLicenseRequestExtensionTests
        {
            [Test]
            public void GivenNoUserIdValue_ShouldThrowSubmarineMappingException()
            {
                // Arrange
                var request = new CreateLicenseRequest();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.Throws<MappingException>(() => request.ToDto());

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.MappingException));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.Mapping.Failed));
                });
            }

            [Test]
            public void GivenValidRequest_ShouldReturnDto()
            {
                // Arrange
                var request = new CreateLicenseRequest
                {
                    UserId = Guid.NewGuid()
                };
                
                // Act
                var result = request.ToDto();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.UserId, Is.EqualTo(request.UserId));
                });
            }
        }
    }
}