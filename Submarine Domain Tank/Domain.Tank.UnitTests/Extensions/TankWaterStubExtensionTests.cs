using System;
using System.Collections.Generic;
using System.Linq;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Water;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;
using Diagnosea.Submarine.Domain.Tank.Extensions;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Extensions
{
    [TestFixture]
    public class TankWaterStubExtensionTests
    {
        public class ToDto : TankWaterStubExtensionTests
        {
            [Test]
            public void GivenTankWaterStub_ReturnsTankWaterDto()
            {
                // Arrange
                var tankWater = new TankWaterStub
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
                };
                
                // Act
                var result = tankWater.ToDto();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.WaterId, Is.EqualTo(tankWater.WaterId));
                    Assert.That(result.Stage, Is.EqualTo(tankWater.Stage));

                    var tankWaterLevel = tankWater.Levels.FirstOrDefault();
                    var resultingTankWaterLevel = result.Levels.FirstOrDefault();
                    Assert.That(resultingTankWaterLevel, Is.Not.Null);
                    Assert.That(resultingTankWaterLevel.Metric, Is.EqualTo(tankWaterLevel.Metric));
                    Assert.That(resultingTankWaterLevel.Quantity, Is.EqualTo(tankWaterLevel.Quantity));
                });
            }
        }
    }
}