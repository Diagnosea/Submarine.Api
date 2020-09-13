using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Api.Abstractions.Attributes;
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
                var roles = new [] {UserRole.Administrator};
                
                // Act
                var attribute = new SubmarineAuthorize(roles);
                
                // Assert
                Assert.That(attribute.Roles, Is.EqualTo(UserRole.Administrator.ToString()));
            }
        }
    }
}