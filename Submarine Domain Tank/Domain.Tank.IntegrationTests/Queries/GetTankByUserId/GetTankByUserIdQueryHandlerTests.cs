using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.Abstractions;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.Tank.Queries.GetTankByUserId;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.Tank.IntegrationTests.Queries.GetTankByUserId
{
    [TestFixture]
    public class GetTankByUserIdQueryHandlerTests : MongoIntegrationTests
    {
        private IMongoCollection<TankEntity> _tankCollection { get; set; }
        private GetTankByUserIdQueryHandler _classUnderTest { get; set; }

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _tankCollection = MongoDatabase.GetCollection<TankEntity>(CollectionConstants.TankCollectionName);
            _classUnderTest = new GetTankByUserIdQueryHandler(MongoDatabase);
        }

        [TearDown]
        public void TearDown()
        {
            _tankCollection.DeleteMany(FilterDefinition<TankEntity>.Empty);
        }

        public class Handle : GetTankByUserIdQueryHandlerTests
        {
            [Test]
            public async Task GivenTankWithoutUserId_ReturnsNull()
            {
                // Arrange
                var query = new GetTankByUserIdQuery
                {
                    UserId = Guid.NewGuid()
                };

                var tank = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };

                await _tankCollection.InsertOneAsync(tank);
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                Assert.That(result, Is.Null);
            }

            [Test]
            public async Task GivenTankWithUserId_ReturnsTank()
            {
                // Arrange
                var userId = Guid.NewGuid();

                var query = new GetTankByUserIdQuery
                {
                    UserId = userId
                };

                var tank = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };

                await _tankCollection.InsertOneAsync(tank);
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.Not.Null);
                    Assert.That(result.UserId, Is.EqualTo(userId));
                });
            }
        }
    }
}