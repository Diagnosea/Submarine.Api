using Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries
{
    [TestFixture]
    public class ValidateAudienceQueryBuilderTests
    {
        public class WithAudienceId : ValidateAudienceQueryBuilderTests
        {
            [Test]
            public void GivenAudienceId_BuildsWithAudienceId()
            {
                // Arrange
                var builder = new ValidateAudienceQueryBuilder();
                const string audienceId = "This is an audience id";
                
                // Act
                var resultingReturn = builder.WithAudienceId(audienceId);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.AudienceId, Is.EqualTo(audienceId));
                });
            }
        }
    }
}