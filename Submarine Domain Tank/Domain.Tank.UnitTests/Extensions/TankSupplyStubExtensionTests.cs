using System;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Tank;
using Diagnosea.Submarine.Domain.Tank.Entities.TankSupply;
using Diagnosea.Submarine.Domain.Tank.Extensions;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Extensions
{
    [TestFixture]
    public class TankSupplyStubExtensionTests
    {
        public class ToDto : TankSupplyStubExtensionTests
        {
            [Test]
            public void GivenTankSupplyStub_ReturnsTankSupplyDto()
            {
                // Arrange
                var tankSupply = new FilterTankSupplyStub
                {
                    SupplyId = Guid.NewGuid(),
                    Name = "This is a tank supply.",
                    Component = TankSupplyComponent.Filter,
                    Metric = Metric.Units,
                    Quantity = 2
                };
                
                // Act
                var result = tankSupply.ToDto();
                
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