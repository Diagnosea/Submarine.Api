using System;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using NUnit.Framework;

namespace Submarine.Domain.License.UnitTests.Commands.InsertLicense
{
    [TestFixture]
    public class InsertLicenseProductCommandBuilderTests
    {
        public class WithName : InsertLicenseProductCommandBuilderTests
        {
            [Test]
            public void GivenName_BuildsWithName()
            {
                // Arrange
                var builder = new InsertLicenseProductCommandBuilder();
                const string name = "This is the name";
                
                // Act
                var result = builder.WithName(name);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.Name, Is.EqualTo(name));
                });
            }
        }

        public class WithKey : InsertLicenseProductCommandBuilderTests
        {
            [Test]
            public void GivenKey_BuildsWitKey()
            {
                // Arrange
                var builder = new InsertLicenseProductCommandBuilder();
                const string key = "This is the key";
                
                // Act
                var result = builder.WithKey(key);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.Key, Is.EqualTo(key));
                });
            }
        }
        
        public class WithCreated : InsertLicenseProductCommandBuilderTests
        {
            [Test]
            public void GivenKey_BuildsWitKey()
            {
                // Arrange
                var builder = new InsertLicenseProductCommandBuilder();
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

        public class WithExpiration : InsertLicenseProductCommandBuilderTests
        {
            [Test]
            public void GivenExpiration_BuildsWithExpiration()
            {
                // Arrange
                var builder = new InsertLicenseProductCommandBuilder();
                var expiration = DateTime.UtcNow.AddDays(1);
                
                // Act
                var result = builder.WithExpiration(expiration);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(builder));
                    Assert.That(build.Expiration, Is.EqualTo(expiration));
                });
            }
        }
    }
}