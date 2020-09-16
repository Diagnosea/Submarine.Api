using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Extensions;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Extensions
{
    [TestFixture]
    public class InsertUserCommandExtensionTests
    {
        public class ToEntity : InsertUserCommandExtensionTests
        {
            [Test]
            public void GivenInsertUserCommand_ReturnsUserEntity()
            {
                // Arrange
                var insertUserCommand = new InsertUserCommand
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "This is an email address",
                    Password = "THis is a password",
                    UserName = "This is a user name",
                    FriendlyName = "This is a friendly name",
                    Roles = new List<UserRole> {UserRole.Standard}
                };
                
                // Act
                var result = insertUserCommand.ToEntity();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(insertUserCommand.Id));
                    Assert.That(result.EmailAddress, Is.EqualTo(insertUserCommand.EmailAddress));
                    Assert.That(result.Password, Is.EqualTo(insertUserCommand.Password));
                    Assert.That(result.UserName, Is.EqualTo(insertUserCommand.UserName));
                    Assert.That(result.FriendlyName, Is.EqualTo(insertUserCommand.FriendlyName));
                    CollectionAssert.Contains(result.Roles, UserRole.Standard);
                });
            }
        }
    }
}