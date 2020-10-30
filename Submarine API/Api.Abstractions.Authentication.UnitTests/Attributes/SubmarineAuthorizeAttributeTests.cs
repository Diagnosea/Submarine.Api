using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Api.Abstractions.Authentication.Attributes;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.UnitTests.Attributes
{
    [TestFixture]
    public class SubmarineAuthorizeAttributeTests
    {
        public class Constructor : SubmarineAuthorizeAttributeTests
        {
            [Test]
            public void GivenRoles_AppliesRoles()
            {
                // Arrange
                var roles = new [] {UserRole.Licenser};
                
                // Act
                var attribute = new SubmarineAuthorize(roles);
                
                // Assert
                Assert.That(attribute.Roles.Contains(UserRole.Administrator.ToString()));
            }

            [Test]
            public void GivenRoles_AppliesRolesPlusAdministrator()
            {
                // Arrange
                var roles = new [] {UserRole.Licenser};
                
                // Act
                var attribute = new SubmarineAuthorize(roles);
                
                // Assert
                var stringifiedRoles = string.Join(", ", UserRole.Licenser, UserRole.Administrator);
                
                Assert.That(attribute.Roles, Is.EqualTo(stringifiedRoles));
            }
        }
    }
}