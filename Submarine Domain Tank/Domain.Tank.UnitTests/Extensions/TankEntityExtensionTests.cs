using System;
using System.Collections.Generic;
using System.Linq;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Abstractions.Enums.Tank;
using Diagnosea.Submarine.Abstractions.Enums.Water;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.Tank.Entities.TankSupply;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;
using Diagnosea.Submarine.Domain.Tank.Extensions;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Extensions
{
    [TestFixture]
    public class TankEntityExtensionTests
    {
        public class ToDto : TankEntityExtensionTests
        {
            [Test]
            public void GivenTankEntity_ReturnsTankDto()
            {
                // Arrange
                var tank = new TankEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
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
                
                // Act
                var result = tank.ToDto();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(tank.Id));
                    Assert.That(result.Name, Is.EqualTo(tank.Name));
                    
                    Assert.That(result.WaterList.WaterId, Is.EqualTo(tank.Water.WaterId));
                    Assert.That(result.WaterList.Stage, Is.EqualTo(tank.Water.Stage));

                    var tankWaterLevel = tank.Water.Levels.FirstOrDefault();
                    var resultingTankWaterLevel = result.WaterList.Levels.FirstOrDefault();
                    Assert.That(resultingTankWaterLevel, Is.Not.Null);
                    Assert.That(resultingTankWaterLevel.Metric, Is.EqualTo(tankWaterLevel.Metric));
                    Assert.That(resultingTankWaterLevel.Quantity, Is.EqualTo(tankWaterLevel.Quantity));

                    var livestock = tank.Livestock.FirstOrDefault();
                    var resultingLivestock = result.Livestock.FirstOrDefault();
                    Assert.That(resultingLivestock.LivestockId, Is.EqualTo(livestock.LivestockId));
                    Assert.That(resultingLivestock.Name, Is.EqualTo(livestock.Name));
                    Assert.That(resultingLivestock.Sex, Is.EqualTo(livestock.Sex));
                    Assert.That(resultingLivestock.Happiness, Is.EqualTo(livestock.Happiness));
                    Assert.That(resultingLivestock.Healthy, Is.EqualTo(livestock.Healthy));
                    Assert.That(resultingLivestock.LastFed, Is.EqualTo(livestock.LastFed));
                    Assert.That(resultingLivestock.UntilNextFeed, Is.EqualTo(livestock.UntilNextFeed));

                    var supply = tank.Supplies.FirstOrDefault();
                    var resultingSupply = result.Supplies.FirstOrDefault();
                    Assert.That(resultingSupply.SupplyId, Is.EqualTo(supply.SupplyId));
                    Assert.That(resultingSupply.Name, Is.EqualTo(supply.Name));
                    Assert.That(resultingSupply.Component, Is.EqualTo(supply.Component));
                    Assert.That(resultingSupply.Metric, Is.EqualTo(supply.Metric));
                    Assert.That(resultingSupply.Quantity, Is.EqualTo(supply.Quantity));
                });
            }
        }
    }
}