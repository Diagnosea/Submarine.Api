using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Tank
{
    [TestFixture]
    public class TankWaterLevelDtoExtensionTests
    {
        public class ToResponse : TankWaterLevelDtoExtensionTests
        {
            public void GivenTankWaterLevelDto_ReturnsTankWaterLevelResponse()
            {
                // Arrange
                var tankWaterLevel = new TankWaterLevelListDto
                {
                    Metric = Metric.Units,
                    Quantity = 23
                };
                
                // Act
                var result = tankWaterLevel.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Metric, Is.EqualTo(tankWaterLevel.Metric));
                    Assert.That(result.Quantity, Is.EqualTo(tankWaterLevel.Quantity));
                });
            }
        }
    }
}