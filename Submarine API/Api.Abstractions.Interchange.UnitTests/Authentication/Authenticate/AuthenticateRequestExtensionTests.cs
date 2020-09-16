using Diagnosea.Submarine.Abstractions.Interchange.Authentication.Authenticate;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Authenticate;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Authentication.Authenticate
{
    [TestFixture]
    public class AuthenticateRequestExtensionTests
    {
        public class ToDto
        {
            [Test]
            public void GivenAuthenticateRequest_ReturnsDto()
            {
                // Arrange
                var request = new AuthenticateRequest
                {
                    EmailAddress = "This is an email address",
                    Password = "This is a password"
                };
                
                // Act
                var result = request.ToDto();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.EmailAddress, Is.EqualTo(request.EmailAddress));
                    Assert.That(result.PlainTextPassword, Is.EqualTo(request.Password));
                });
            }
        }
    }
}