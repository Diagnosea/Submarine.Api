using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Abstractions.Enums.Tank;
using Diagnosea.Submarine.Abstractions.Enums.Water;
using Diagnosea.Submarine.Domain.Instructors.Tank;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.Tank.Entities.TankSupply;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;
using Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using Diagnosea.Submarine.UnitTestPack.Extensions;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Diagnosea.Domain.Instructors.UnitTests.Instructors
{
    [TestFixture]
    public class TankInstructorTests
    {
        private Mock<IMediator> _mediator;
        private TankInstructor _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new TankInstructor(_mediator.Object);
        }
        
        public class GetByUserId : TankInstructorTests
        {
            [Test]
            public void GivenInvalidUserId_ThrowsEntityNotFoundException()
            {
                var userId = Guid.NewGuid();

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>();
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(
                        () => _classUnderTest.GetByUserIdAsync(userId, CancellationToken.None));
                    
                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.User.UserNotFound));
                });
            }

            [Test]
            public void GivenNoTankWithUserId_ThrowsEntityNotFoundException()
            {
                // Arrange
                var userId = Guid.NewGuid();

                var user = new UserEntity
                {
                    Id = userId
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);

                _mediator
                    .SetupHandler<GetTanksByUserIdQuery, IList<TankEntity>>();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(
                        () => _classUnderTest.GetByUserIdAsync(userId, CancellationToken.None));
                    
                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.Tank.NoTanksWithUserId));
                });
            }
            
            [Test]
            public async Task GivenTankWithUserId_ReturnsLicense()
            {
                // Arrange
                var tankId = Guid.NewGuid();
                var userId = Guid.NewGuid();

                var user = new UserEntity
                {
                    Id = userId
                };

                var tank =  new TankEntity
                {
                    Id = tankId,
                    UserId = userId,
                    Name = "This is a tank.",
                    Water = new TankWaterStub
                    {
                        WaterId = Guid.NewGuid(),
                        Stage = WaterCycleStage.BuildingAnaerobic,
                        Levels = new List<TankWaterLevelStub>
                        {
                            new TankWaterLevelStub
                            {
                                Metric = Metric.Units,
                                Quantity = 23
                            }
                        }
                    },
                    Livestock = new List<TankLivestockStub>
                    {
                        new TankLivestockStub
                        {
                            LivestockId = Guid.NewGuid(),
                            Name = "This is a livestock.",
                            Sex = LivestockSex.Female,
                            Happiness = TankLivestockHappiness.Happy,
                            Healthy = true,
                            LastFed = DateTime.UtcNow,
                            UntilNextFeed = new TimeSpan(1, 1, 1, 1, 1)
                        }
                    },
                    Supplies = new List<TankSupplyStub>
                    {
                        new TankSupplyStub
                        {
                            SupplyId = Guid.NewGuid(),
                            Name = "This is a supply.",
                            Component = TankSupplyComponent.Filter,
                            Metric = Metric.Units,
                            Quantity = 23
                        }
                    }
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);

                _mediator
                    .SetupHandler<GetTanksByUserIdQuery, IList<TankEntity>>()
                    .ReturnsAsync(new List<TankEntity> {tank});
                
                // Act
                var result = await _classUnderTest.GetByUserIdAsync(userId, CancellationToken.None);
                
                // Assert
                var resultingTank = result.FirstOrDefault();
                
                Assert.That(resultingTank.Id, Is.EqualTo(tankId));
            }
        }
    }
}