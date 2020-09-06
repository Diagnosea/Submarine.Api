using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosae.Submarine.Domain.User.IntegrationTests.Queries
{
    [TestFixture]
    public class GetUserByEmailQueryHandlerTests : UserIntegrationTests
    {
        private IMongoCollection<UserEntity> _userCollection;
        private GetUserByEmailQueryHandler _classUnderTests;

        [SetUp]
        public void SetUp()
        {
            _userCollection = Database.GetEntityCollection<UserEntity>();
            _classUnderTests = new GetUserByEmailQueryHandler(Database);
        }

        [TearDown]
        public void TearDown()
        {
            _userCollection.DeleteMany(FilterDefinition<UserEntity>.Empty);
        }

        public class Handle : GetUserByEmailQueryHandlerTests
        {
            [Test]
            public async Task GivenGetUserByEmailQuery_ReturnsUserFromMongo()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var getUserByEmailQuery = new GetUserByEmailQuery
                {
                    EmailAddress = "john.smith@gmail.com"
                };

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "john.smith@gmail.com",
                    Password = "30=5902i0jfe-q0dj-0",
                    UserName = "Johnoo2398",
                    FriendlyName = "John Smith",
                    Roles = new List<UserRole> {UserRole.Standard}
                };

                await _userCollection.InsertOneAsync(user, null, cancellationToken);
                
                // Act
                var result = await _classUnderTests.Handle(getUserByEmailQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(user.Id));
                    Assert.That(result.EmailAddress, Is.EqualTo(user.EmailAddress));
                    Assert.That(result.Password, Is.Null);
                    Assert.That(result.UserName, Is.Null);
                    Assert.That(result.FriendlyName, Is.Null);
                    Assert.That(result.Roles, Is.Empty);
                });
            }
        }
    }
}