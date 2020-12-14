using System;
using System.Collections.Generic;
using System.Linq;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Water;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Tank
{
    [TestFixture]
    public class TankWaterDtoExtensionTests
    {
        public class ToResponse : TankWaterLevelDtoExtensionTests
        {
            [Test]
            public void GivenTankWaterDto_ReturnsTankWaterResponse()
            {
                // Arrange
                var tankWater = new TankWaterDto
                {
                    WaterId = Guid.NewGuid(),
                    Stage = WaterCycleStage.BuildingAnaerobic,
                    Levels = new List<TankWaterLevelDto>
                    {
                        new TankWaterLevelDto
                        {
                            Metric = Metric.Units,
                            Quantity = 23
                        }
                    }
                };
                
                // Act
                var result = tankWater.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.WaterId, Is.EqualTo(tankWater.WaterId));
                    Assert.That(result.Stage, Is.EqualTo(tankWater.Stage));

                    var tankWaterLevel = tankWater.Levels.FirstOrDefault();
                    var resultingTankWaterLevel = result.Levels.FirstOrDefault();
                    Assert.That(resultingTankWaterLevel.Metric, Is.EqualTo(tankWaterLevel.Metric));
                    Assert.That(resultingTankWaterLevel.Quantity, Is.EqualTo(tankWaterLevel.Quantity));
                });
            }
        }
    }
}