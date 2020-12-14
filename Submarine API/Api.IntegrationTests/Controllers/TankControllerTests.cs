using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.IntegrationTestPack;
using Diagnosea.IntegrationTestPack.Builders;
using Diagnosea.IntegrationTestPack.Extensions;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Abstractions;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.User.Entities;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class TankControllerTests : WebApiIntegrationTests<Startup>
    {
        private IMongoCollection<UserEntity> _userCollection;
        private IMongoCollection<TankEntity> _tankCollection;

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _userCollection = MongoDatabase.GetCollection<UserEntity>(CollectionConstants.UserCollectionName);
            _tankCollection = MongoDatabase.GetCollection<TankEntity>(CollectionConstants.TankCollectionName);
        }

        [TearDown]
        public void TearDown()
        {
            HttpClient.ClearBearerToken();

            _userCollection.DeleteMany(FilterDefinition<UserEntity>.Empty);
            _tankCollection.DeleteMany(FilterDefinition<TankEntity>.Empty);
        }

        public class GetByUserId : TankControllerTests
        {
            private readonly string _url = $"{RouteConstants.Version1}/{RouteConstants.Tank.Base}/{RouteConstants.Tank.Me}";

            [Test]
            public async Task GivenNoBearerToken_RespondsWithUnauthorized()
            {
                // Act
                var response = await HttpClient.GetAsync(_url);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            }

            [Test]
            public async Task GivenInvalidRole_RespondsWithForbidden()
            {
                // Arrange
                var bearerToken = new TestBearerTokenBuilder()
                    .Build();

                HttpClient.SetBearerToken(bearerToken);
                
                // Act
                var response = await HttpClient.GetAsync(_url);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            }

            [Test]
            public async Task GivenUserDoesNotExist_RespondsWithNotFound()
            {
                // Arrange
                var userId = Guid.NewGuid();
                
                var bearerToken = new TestBearerTokenBuilder()
                    .WithUserId(userId)
                    .WithRole(UserRole.Standard)
                    .Build();
                
                HttpClient.SetBearerToken(bearerToken);
                
                // Act
                var response = await HttpClient.GetAsync(_url);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
                });
            }

            [Test]
            public async Task GivenTankDoesNotExist_RespondsWithNotFound()
            {
                // Arrange
                var userId = Guid.NewGuid();
                
                var bearerToken = new TestBearerTokenBuilder()
                    .WithUserId(userId)
                    .WithRole(UserRole.Standard)
                    .Build();
                
                HttpClient.SetBearerToken(bearerToken);

                var user = new UserEntity
                {
                    Id = userId
                };

                await _userCollection.InsertOneAsync(user);
                
                // Act
                var response = await HttpClient.GetAsync(_url);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(TankExceptionMessages.NoTankWithUserId));
                });
            }

            [Test]
            public async Task GivenTankDoesExist_RespondsWithTank()
            {
                // Arrange
                var userId = Guid.NewGuid();
                var tankId = Guid.NewGuid();
                
                var bearerToken = new TestBearerTokenBuilder()
                    .WithUserId(userId)
                    .WithRole(UserRole.Standard)
                    .Build();
                
                HttpClient.SetBearerToken(bearerToken);

                var user = new UserEntity
                {
                    Id = userId
                };

                var tank = new TankEntity
                {
                    Id = tankId,
                    UserId = userId
                };

                await _userCollection.InsertOneAsync(user);
                await _tankCollection.InsertOneAsync(tank);
                
                // Act
                var response = await HttpClient.GetAsync(_url);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<TankResponse>();

                Assert.That(responseData.Id, Is.EqualTo(tankId));
            }
        }
    }
}