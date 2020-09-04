using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Entities;
using Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.Authentication.IntegrationTests.Queries
{
    [TestFixture]
    public class ValidateAudienceQueryHandlerTests : AuthenticationIntegrationTests
    {
        private IMongoCollection<AudienceEntity> _audienceCollection;
        private ValidateAudienceQueryHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _audienceCollection = Database.GetEntityCollection<AudienceEntity>();
            _classUnderTest = new ValidateAudienceQueryHandler(Database);
        }

        [TearDown]
        public void TearDown()
        {
            _audienceCollection.DeleteMany(FilterDefinition<AudienceEntity>.Empty);
        }

        public class Handle : ValidateAudienceQueryHandlerTests
        {
            [Test]
            public async Task GivenAudienceExists_ShouldReturnTrue()
            {
                // Arrange
                var audience = new AudienceEntity
                {
                    Id = "This is an audience identifier"
                };

                await _audienceCollection.InsertOneAsync(audience);

                var cancellationToken = new CancellationToken();
                
                var query = new ValidateAudienceQuery
                {
                    AudienceId = audience.Id
                };
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.That(result, Is.True);
            }

            [Test]
            public async Task GivenAudienceDoesntExist_ShouldReturnFalse()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var query = new ValidateAudienceQuery
                {
                    AudienceId = "This is an audience ID"
                };
                
                // Act
                var result = await _classUnderTest.Handle(query, cancellationToken);
                
                // Assert
                Assert.That(result, Is.False);
            }
        }
    }
}