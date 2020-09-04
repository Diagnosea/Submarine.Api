using System;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Queries
{
    [TestFixture]
    public class GetUserByIdQueryBuilderTests
    {
        public class WithId : GetUserByIdQueryBuilderTests
        {
            [Test]
            public void GivenId_BuildsWithId()
            {
                // Arrange
                var builder = new GetUserByIdQueryBuilder();
                var id = Guid.NewGuid();
                
                // Act
                var resultingReturn = builder.WithId(id);
                var resultingBuild = resultingReturn.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Id, Is.EqualTo(id));
                });
            }
        }
    }
}