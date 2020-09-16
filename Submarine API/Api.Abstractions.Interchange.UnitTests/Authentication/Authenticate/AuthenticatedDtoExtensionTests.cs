using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Authenticate;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Authentication.Authenticate
{
    [TestFixture]
    public class AuthenticatedDtoExtensionTests
    {
        public class ToResponse
        {
            [Test]
            public void GivenAuthenticatedDto_ReturnsResponse()
            {
                // Arrange
                var authenticated = new AuthenticatedDto
                {
                    BearerToken = "This is a bearer token"
                };
                
                // Act
                var result = authenticated.ToResponse();
                
                // Assert
                Assert.That(result.BearerToken, Is.EqualTo(authenticated.BearerToken));
            }
        }
    }
}