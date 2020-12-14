using System;
using Abstractions.Exceptions;
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
            public void GivenWithoutUserId_ThrowsMappingException()
            {
                // Arrange
                var builder = new GetTankByUserIdQueryBuilder();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.Throws<SubmarineMappingException>(() => builder.Build());

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.MappingException));
                    Assert.That(exception.UserMessage, Is.EqualTo(MappingExceptionMessages.Failed));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                });
            }
            
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