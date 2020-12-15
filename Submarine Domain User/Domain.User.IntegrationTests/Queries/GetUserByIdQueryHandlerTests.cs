using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.IntegrationTests.Queries
{
    [TestFixture]
    public class GetUserByIdQueryHandlerTests : UserIntegrationTests
    {
        private IMongoCollection<UserEntity> _userCollection;
        private GetUserByIdQueryHandler _classUnderTests;

        [SetUp]
        public void SetUp()
        {
            _userCollection = Database.GetCollection<UserEntity>("User");
            _classUnderTests = new GetUserByIdQueryHandler(Database);
        }

        public class Handle : GetUserByIdQueryHandlerTests
        {
            [Test]
            public async Task GivenGetUserByIdQuery_WithInvalidId_ReturnsNull()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var getUserByIdQuery = new GetUserByIdQuery
                {
                    Id = Guid.NewGuid()
                };

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "john.smith@gmail.com",
                    Password = "30=5902i0jfe-q0dj-0",
                    UserName = "John2398",
                    FriendlyName = "John Smith",
                    Roles = new List<UserRole> {UserRole.Standard}
                };

                await _userCollection.InsertOneAsync(user, null, cancellationToken);
                
                // Act
                var result = await _classUnderTests.Handle(getUserByIdQuery, cancellationToken);
                
                // Assert
                Assert.That(result, Is.Null);
            }
            
            [Test]
            public async Task GivenGetUserByIdQuery_WithValidId_ReturnsUserFromMongo()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var getUserByIdQuery = new GetUserByIdQuery
                {
                    Id = Guid.NewGuid()
                };

                var user = new UserEntity
                {
                    Id = getUserByIdQuery.Id,
                    EmailAddress = "john.smith@gmail.com",
                    Password = "30=5902i0jfe-q0dj-0",
                    UserName = "John2398",
                    FriendlyName = "John Smith",
                    Roles = new List<UserRole> {UserRole.Standard}
                };

                await _userCollection.InsertOneAsync(user, null, cancellationToken);
                
                // Act
                var result = await _classUnderTests.Handle(getUserByIdQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(user.Id));
                    Assert.That(result.EmailAddress, Is.Null);
                    Assert.That(result.Password, Is.Null);
                    Assert.That(result.UserName, Is.EqualTo(user.UserName));
                    Assert.That(result.FriendlyName, Is.EqualTo(user.FriendlyName));
                    Assert.That(result.Roles, Is.Empty);
                });
            }
        }
    }
}