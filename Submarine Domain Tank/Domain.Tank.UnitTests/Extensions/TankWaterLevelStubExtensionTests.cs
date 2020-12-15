using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;
using Diagnosea.Submarine.Domain.Tank.Extensions;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Extensions
{
    [TestFixture]
    public class TankWaterLevelStubExtensionTests
    {
        public class ToDto : TankWaterLevelStubExtensionTests
        {
            [Test]
            public void GivenTankWaterLevelStub_ReturnsTankWaterLevelDto()
            {
                // Arrange
                var tankWaterLevel = new TankWaterBacterialLevelStub
                {
                    Bacteria = Bacteria.Ammonia,
                    Metric = Metric.Units,
                    Quantity = 23
                };
                
                // Act
                var result = tankWaterLevel.ToDto();
                
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