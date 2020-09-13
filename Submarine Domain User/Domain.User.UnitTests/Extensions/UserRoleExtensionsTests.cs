using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Extensions;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Extensions
{
    [TestFixture]
    public class UserRoleExtensionsTests
    {
        public new class ToString : UserRoleExtensionsTests
        {
            [Test]
            public void GivenRoles_ReturnsStringRoles()
            {
                // Arrange
                var roles = new List<UserRole> {UserRole.Standard, UserRole.Administrator};
                
                // Act
                var result = roles.AsStrings();
                
                // Assert
                Assert.Multiple(() =>
                {
                    CollectionAssert.Contains(result, UserRole.Standard.ToString());
                    CollectionAssert.Contains(result, UserRole.Administrator.ToString());
                });
            }
        }
    }
}