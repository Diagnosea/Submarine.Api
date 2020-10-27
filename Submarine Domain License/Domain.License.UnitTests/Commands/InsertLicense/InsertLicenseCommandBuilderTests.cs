using System;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using NUnit.Framework;

namespace Submarine.Domain.License.UnitTests.Commands.InsertLicense
{
    [TestFixture]
    public class InsertLicenseCommandBuilderTests
    {
        public class WithId : InsertLicenseCommandBuilderTests
        {
            [Test]
            public void GivenId_BuildsWithId()
            {
                // Arrange
                var builder = new InsertLicenseCommandBuilder();
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

        public class WithCreated : InsertLicenseCommandBuilderTests
        {
            [Test]
            public void GivenCreated_BuildWithCreated()
            {
                // Arrange
                var builder = new InsertLicenseCommandBuilder();
                var created = DateTime.UtcNow;
                
                // Act
                var result = builder.WithCreated(created);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.Created, Is.EqualTo(created));
                });
            }
        }

        public class WithUserId : InsertLicenseCommandBuilderTests
        {
            [Test]
            public void GivenUserId_BuildsWithUserId()
            {
                // Arrange
                var builder = new InsertLicenseCommandBuilder();
                var userId = Guid.NewGuid();
                
                // Act
                var result = builder.WithUserId(userId);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.UserId, Is.EqualTo(userId));
                });
            }
        }

        public class WithProduct : InsertLicenseCommandBuilderTests
        {
            [Test]
            public void GivenInsertLicenseProductCommand_BuildsWithInsertLicenseProductCommand()
            {
                // Arrange
                var builder = new InsertLicenseCommandBuilder();
                var insertLicenseProductCommand = new InsertLicenseProductCommand();
                
                // Act
                var result = builder.WithProduct(insertLicenseProductCommand);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    CollectionAssert.Contains(build.Products, insertLicenseProductCommand);
                });
            }
        }
    }
}