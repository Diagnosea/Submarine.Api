using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries.GenerateBearerToken
{
    [TestFixture]
    public class GenerateBearerTokenQueryBuilderTests
    {
        public class WithSubject : GenerateBearerTokenQueryBuilderTests
        {
            [Test]
            public void GivenSubject_BuildsWithSubject()
            {
                // Arrange
                var builder = new GenerateBearerTokenQueryBuilder();
                const string subject = "This is the subject";
                
                // Act
                var resultingReturn = builder.WithSubject(subject);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Subject, Is.EqualTo(subject));
                });
            }
        }

        public class WithName : GenerateBearerTokenQueryBuilderTests
        {
            [Test]
            public void GivenName_BuildsWithName()
            {
                // Arrange
                var builder = new GenerateBearerTokenQueryBuilder();
                const string name = "This is the name";
                
                // Act
                var resultingReturn = builder.WithName(name);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Name, Is.EqualTo(name));
                });
            }
        }

        public class WithAudienceId : GenerateBearerTokenQueryBuilderTests
        {
            [Test]
            public void GivenAudienceId_ReturnsAudienceId()
            {
                // Arrange
                var builder = new GenerateBearerTokenQueryBuilder();
                const string audienceId = "This is the audience";
                
                // Act
                var resultingReturn = builder.WithAudience(audienceId);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.AudienceId, Is.EqualTo(audienceId));
                });
            }
        }

        public class WithRoles : GenerateBearerTokenQueryBuilderTests
        {
            [Test]
            public void GivenRoles_BuildsWithRoles()
            {
                // Arrange
                var builder = new GenerateBearerTokenQueryBuilder();
                var roles = new[] {"this is a role", "this is also a role"};
                
                // Act
                var resultingReturn = builder.WithRoles(roles);
                var resultingBuild = builder.Build();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(resultingReturn, Is.EqualTo(builder));
                    Assert.That(resultingBuild.Roles, Is.EqualTo(roles));
                });
            }
        }
    }
}