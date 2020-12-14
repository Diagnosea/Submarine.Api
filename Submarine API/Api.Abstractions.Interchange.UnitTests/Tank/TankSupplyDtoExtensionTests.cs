using System;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Tank;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Tank
{
    [TestFixture]
    public class TankSupplyDtoExtensionTests
    {
        public class ToResponse : TankSupplyDtoExtensionTests
        {
            [Test]
            public void GivenTankSupplyDto_ReturnsTankSupplyResponse()
            {
                // Arrange
                var tankSupply = new TankSupplyDto
                {
                    SupplyId = Guid.NewGuid(),
                    Name = "This is a supply",
                    Component = TankSupplyComponent.Filter,
                    Metric = Metric.Units,
                    Quantity = 23
                };
                
                // Act
                var result = tankSupply.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.SupplyId, Is.EqualTo(tankSupply.SupplyId));
                    Assert.That(result.Name, Is.EqualTo(tankSupply.Name));
                    Assert.That(result.Component, Is.EqualTo(tankSupply.Component));
                    Assert.That(result.Metric, Is.EqualTo(tankSupply.Metric));
                    Assert.That(result.Quantity, Is.EqualTo(tankSupply.Quantity));
                });
            }
        }
    }
}