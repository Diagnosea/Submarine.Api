using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Authentication.Register
{
    [TestFixture]
    public class RegisterRequestExtensionTests
    {
        public class ToDto : RegisterRequestExtensionTests
        {
            [Test]
            public void GivenRegisterRequest_ReturnsRegisterDto()
            {
                // Arrange
                var register = new RegisterRequest
                {
                    EmailAddress = "This is an email address",
                    Password = "This is a password"
                };
                
                // Act
                var result = register.ToDto();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.EmailAddress, Is.EqualTo(register.EmailAddress));
                    Assert.That(result.PlainTextPassword, Is.EqualTo(register.Password));
                });
            }
        }
    }
}