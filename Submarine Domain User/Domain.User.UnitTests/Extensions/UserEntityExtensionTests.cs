using System;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Extensions;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Extensions
{
    [TestFixture]
    public class UserEntityExtensionTests
    {
        public class ToDto : UserEntityExtensionTests
        {
            [Test]
            public void GivenUserEntity_ReturnsUserDto()
            {
                // Arrange
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    UserName = "This is a user name",
                    FriendlyName = "This is a friendly name"
                };
                
                // Act
                var result = user.ToDto();
                
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