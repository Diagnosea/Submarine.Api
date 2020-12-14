using System;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Abstractions.Enums.Tank;
using Diagnosea.Submarine.Api.Abstractions.Interchange.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.UnitTests.Tank
{
    [TestFixture]
    public class TankLivestockDtoExtensionTests
    {
        public class ToResponse : TankLivestockDtoExtensionTests
        {
            [Test]
            public void GivenTankLivestockDto_ReturnsTankLivestockResponse()
            {
                // Arrange
                var tankLivestock = new TankLivestockDto
                {
                    LivestockId = Guid.NewGuid(),
                    Name = "This is livestock",
                    Sex = LivestockSex.Female,
                    Happiness = TankLivestockHappiness.Happy,
                    Healthy = true,
                    LastFed = DateTime.Now.AddDays(-1),
                    UntilNextFeed = new TimeSpan(1, 1, 1, 1, 1)
                };
                
                // Act
                var result = tankLivestock.ToResponse();
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.LivestockId, Is.EqualTo(tankLivestock.LivestockId));
                    Assert.That(result.Name, Is.EqualTo(tankLivestock.Name));
                    Assert.That(result.Sex, Is.EqualTo(tankLivestock.Sex));
                    Assert.That(result.Happiness, Is.EqualTo(tankLivestock.Happiness));
                    Assert.That(result.Healthy, Is.EqualTo(tankLivestock.Healthy));
                    Assert.That(result.LastFed, Is.EqualTo(tankLivestock.LastFed));
                    Assert.That(result.UntilNextFeed, Is.EqualTo(tankLivestock.UntilNextFeed));
                });
            }
        }
    }
}