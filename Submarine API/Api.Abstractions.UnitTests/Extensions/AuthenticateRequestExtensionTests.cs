using Diagnosea.Submarine.Abstractions.Interchange.Authentication;
using Diagnosea.Submarine.Api.Abstractions.Extensions.Authentication;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.UnitTests.Extensions
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