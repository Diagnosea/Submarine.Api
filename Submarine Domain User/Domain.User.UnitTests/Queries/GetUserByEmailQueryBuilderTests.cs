using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Queries
{
    [TestFixture]
    public class GetUserByEmailQueryBuilderTests
    {
        public class WithEmailAddress : GetUserByEmailQueryBuilderTests
        {
            [Test]
            public void GivenEmailAddress_BuildsWithEmailAddress()
            {
                // Arrange
                var builder = new GetUserByEmailQueryBuilder();
                const string emailAddress = "john.smith@gmail.com";
                
                // Act
                var resultingReturn = builder.WithEmailAddress(emailAddress);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.EmailAddress, Is.EqualTo(emailAddress));
                });
            }
        }
    }
}