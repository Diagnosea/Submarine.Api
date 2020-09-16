using System;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Commands
{
    [TestFixture]
    public class InsertUserCommandBuilderTests
    {
        public class WithId : InsertUserCommandBuilderTests
        {
            [Test]
            public void GivenId_BuildsWithId()
            {
                // Arrange
                var builder = new InsertUserCommandBuilder();
                var id = Guid.NewGuid();
                
                // Act
                var result = builder.WithId(id);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.Id, Is.EqualTo(id));
                });
            }
        }

        public class WithEmailAddress : InsertUserCommandBuilderTests
        {
            [Test]
            public void GivenEmailAddress_BuildsWithEmailAddress()
            {
                // Arrange
                var builder = new InsertUserCommandBuilder();
                const string emailAddress = "john.smith@gmail.com";

                // Act
                var result = builder.WithEmailAddress(emailAddress);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.EmailAddress, Is.EqualTo(emailAddress));
                });
            }
        }

        public class WithPassword : InsertUserCommandBuilderTests
        {
            [Test]
            public void GivenPassword_BuildsWithEmailAddress()
            {
                // Arrange
                var builder = new InsertUserCommandBuilder();
                const string password = "This is a password";
                
                // Act
                var result = builder.WithPassword(password);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.Password, Is.EqualTo(password));
                });
            }
        }

        public class WithUserName : InsertUserCommandBuilderTests
        {
            [Test]
            public void GivenUserName_BuildsWithUserName()
            {
                // Arrange
                var builder = new InsertUserCommandBuilder();
                const string userName = "This is a user name";
                
                // Act
                var result = builder.WithUserName(userName);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.UserName, Is.EqualTo(userName));
                });
            }
        }

        public class WithFriendlyName : InsertUserCommandBuilderTests
        {
            [Test]
            public void GivenFriendlyName_BuildsWithFriendlyName()
            {
                // Arrange
                var builder = new InsertUserCommandBuilder();
                const string friendlyName = "This is a friendly name";
                
                // Act
                var result = builder.WithFriendlyName(friendlyName);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.FriendlyName, Is.EqualTo(friendlyName));
                });
            }
        }

        public class WithRole : InsertUserCommandBuilderTests
        {
            [Test]
            public void GivenRole_BuildsWithRole()
            {
                // Arrange
                var builder =new InsertUserCommandBuilder();
                
                // Act
                var result = builder.WithRole(UserRole.Standard);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    CollectionAssert.Contains(build.Roles, UserRole.Standard);
                });
            }
        }
    }
}