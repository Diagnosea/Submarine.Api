using System;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId;
using NUnit.Framework;
using Submarine.Domain.License.UnitTests.Queries.GetLicenseByProductKey;

namespace Submarine.Domain.License.UnitTests.Queries.GetLicenseByUserId
{
    [TestFixture]
    public class GetLicenseByUserIdQueryBuilderTests
    {
        public class WithUserId : GetLicenseByProductKeyQueryBuilderTests
        {
            [Test]
            public void GivenUserId_BuildsWithUserId()
            {
                // Arrange
                var builder = new GetLicenseByUserIdQueryBuilder();
                var userId = Guid.NewGuid();
                
                // Act
                var result = builder.WithUserId(userId);
                var build = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.EqualTo(userId));
                    Assert.That(build.UserId, Is.EqualTo(build.UserId));
                });
            }
        }
    }
}