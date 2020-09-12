using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Responses;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.User;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Utilities;
using Diagnosea.Submarine.Utilities.Extensions;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class UserControllerTests : ApiIntegrationTests
    {
        private IMongoCollection<UserEntity> _userCollection;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userCollection = Database.GetEntityCollection<UserEntity>();
        }

        [TearDown]
        public void TearDown()
        {
            Client.ClearBearerToken();
            
            _userCollection.DeleteMany(FilterDefinition<UserEntity>.Empty);
        }

        public class GetUserAsync : UserControllerTests
        {
            private readonly string _url = $"{RouteConstants.Version1}/{RouteConstants.User.Base}";

            private string GetUrl(Guid id) => $"{_url}/{id}";
            
            [Test]
            public async Task GivenBearerToken_RespondsWithUnauthorized()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var url = GetUrl(userId);
                
                // Act
                var response = await Client.GetAsync(url);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            }

            [Test]
            public async Task GivenInvalidRole_RespondsWithForbidden()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var url = GetUrl(userId);
                var bearerToken = new TestBearerTokenBuilder()
                    .Build();
                
                Client.SetBearerToken(bearerToken);
                
                // Act 
                var response = await Client.GetAsync(url);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            }

            [Test]
            public async Task GivenInvalidUserId_RespondsWithNotFound()
            {
                // Arrange - Data
                var userId = Guid.NewGuid();
                var url = GetUrl(userId);
                var bearerToken = new TestBearerTokenBuilder()
                    .WithRole(UserRole.Administrator)
                    .Build();

                // Arrange - Client
                Client.SetBearerToken(bearerToken);

                // Act
                var response = await Client.GetAsync(url);
                
                // Assert
                var responseData =  await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int)SubmarineExceptionCode.EntityNotFound));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
                });
            }

            [Test]
            public async Task GivenValidUserId_RespondsWithUser()
            {
                // Arrange - Data
                var userId = Guid.NewGuid();
                var url = GetUrl(userId);
                var bearerToken = new TestBearerTokenBuilder()
                    .WithRole(UserRole.Administrator)
                    .Build();

                // Arrange - Client
                Client.SetBearerToken(bearerToken);
                
                // Arrange - Entity
                var user = new UserEntity
                {
                    Id = userId
                };

                await _userCollection.InsertOneAsync(user);
                
                // Act
                var response = await Client.GetAsync(url);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<UserResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(responseData.Id, Is.EqualTo(user.Id));
                });
            }
        }
    }
}