using System;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Enums;
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
                var resultingReturn = builder.WithId(id);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Id, Is.EqualTo(id));
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
                var resultingReturn = builder.WithEmailAddress(emailAddress);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.EmailAddress, Is.EqualTo(emailAddress));
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
                var resultingReturn = builder.WithPassword(password);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Password, Is.EqualTo(password));
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
                var resultingReturn = builder.WithUserName(userName);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.UserName, Is.EqualTo(userName));
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
                var resultingReturn = builder.WithRole(UserRole.Standard);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    CollectionAssert.Contains(resultingBuild.Roles, UserRole.Standard);
                });
            }
        }
    }
}