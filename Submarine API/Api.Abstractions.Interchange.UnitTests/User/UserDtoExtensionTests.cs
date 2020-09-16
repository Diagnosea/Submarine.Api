using System;
using Diagnosea.Submarine.Api.Abstractions.Interchange.User;
using Diagnosea.Submarine.Domain.User.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.User
{
    [TestFixture]
    public class UserDtoExtensionTests
    {
        public class ToResponse : UserDtoExtensionTests
        {
            [Test]
            public void GivenUserDto_ReturnsUserResponse()
            {
                // Arrange
                var user = new UserDto
                {
                    Id = Guid.NewGuid(),
                    UserName = "This is a user name",
                    FriendlyName = "This is a friendly name"
                };
                
                // Act
                var result = user.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(user.Id));
                    Assert.That(result.UserName, Is.EqualTo(user.UserName));
                    Assert.That(result.FriendlyName, Is.EqualTo(user.FriendlyName));
                });
            }
        }
    }
}