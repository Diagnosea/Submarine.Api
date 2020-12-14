using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.IntegrationTestPack;
using Diagnosea.Submarine.Domain.Abstractions;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId;
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
            public async Task GivenTankWithoutUserId_ReturnsEmptyCollection()
            {
                // Arrange
                var query = new GetTanksByUserIdQuery
                {
                    UserId = Guid.NewGuid()
                };

                var tankOne = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };
                
                var tankTwo = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid()
                };

                await _tankCollection.InsertManyAsync(new []{tankOne, tankTwo});
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                CollectionAssert.IsEmpty(result);
            }

            [Test]
            public async Task GivenTankWithUserId_ReturnsTank()
            {
                // Arrange
                var userId = Guid.NewGuid();

                var query = new GetTanksByUserIdQuery
                {
                    UserId = userId
                };

                var tankOne = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };
                
                var tankTwo = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                };

                await _tankCollection.InsertManyAsync(new []{tankOne, tankTwo});
                
                // Act
                var result = await _classUnderTest.Handle(query, CancellationToken.None);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Count, Is.EqualTo(2));

                    var resultingTankOne = result.FirstOrDefault();
                    Assert.That(resultingTankOne, Is.Not.Null);
                    Assert.That(resultingTankOne.Id, Is.EqualTo(tankOne.Id));

                    var resultingTankTwo = result.LastOrDefault();
                    Assert.That(resultingTankTwo, Is.Not.Null);
                    Assert.That(resultingTankTwo.Id, Is.EqualTo(tankTwo.Id));
                });
            }
        }
    }
}