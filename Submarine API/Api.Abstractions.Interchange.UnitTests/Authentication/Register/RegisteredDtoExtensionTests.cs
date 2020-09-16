using System;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Authentication.Register
{
    [TestFixture]
    public class RegisteredDtoExtensionTests
    {
        public class ToResponse : RegisteredDtoExtensionTests
        {
            [Test]
            public void GivenRegisteredDto_ReturnsRegisteredResponse()
            {
                // Arrange
                var register = new RegisteredDto
                {
                    UserId = Guid.NewGuid()
                };
                
                // Act
                var result = register.ToResponse();
                
                // Assert
                Assert.That(result.UserId, Is.EqualTo(register.UserId));
            }
        }
    }
}