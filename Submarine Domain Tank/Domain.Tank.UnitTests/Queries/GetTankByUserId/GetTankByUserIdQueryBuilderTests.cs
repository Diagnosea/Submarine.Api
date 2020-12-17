using System;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Queries.GetTankByUserId
{
    [TestFixture]
    public class GetTankByUserIdQueryBuilderTests
    {
        public class WithUserId : GetTankByUserIdQueryBuilderTests
        {
            [Test]
            public void GivenWithoutUserId_ThrowsMappingException()
            {
                // Arrange
                var builder = new GetTanksByUserIdQueryBuilder();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.Throws<MappingException>(() => builder.Build());

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.MappingException));
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.Mapping.Failed));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                });
            }
            
            [Test]
            public void GivenUserId_BuildsWithUserId()
            {
                // Arrange
                var builder = new GetTanksByUserIdQueryBuilder();
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