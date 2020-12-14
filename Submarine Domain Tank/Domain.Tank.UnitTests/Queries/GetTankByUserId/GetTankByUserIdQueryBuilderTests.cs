using System;
using Diagnosea.Submarine.Domain.Tank.Queries.GetTankByUserId;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Queries.GetTankByUserId
{
    [TestFixture]
    public class GetTankByUserIdQueryBuilderTests
    {
        public class WithUserId : GetTankByUserIdQueryBuilderTests
        {
            [Test]
            public void GivenUserId_BuildsWithUserId()
            {
                // Arrange
                var builder = new GetTankByUserIdQueryBuilder();
                var userId = Guid.NewGuid();
                
                // Act
                var resultingReturn = builder.WithUserId(userId);
                var resultingBuild = resultingReturn.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.UserId, Is.EqualTo(userId));
                });
            }
        }
    }
}