using System;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Abstractions.Enums.Tank;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.Tank.Extensions;
using NUnit.Framework;

namespace Domain.Tank.UnitTests.Extensions
{
    [TestFixture]
    public class TankLivestockStubExtensionTests
    {
        public class ToDto : TankLivestockStubExtensionTests
        {
            [Test]
            public void GivenTankLivestockStub_ReturnsTankLivestockDto()
            {
                // Arrange
                var tankLivestock = new TankLivestockStub
                {
                    LivestockId = Guid.NewGuid(),
                    Name = "This is a livestock.",
                    Sex = LivestockSex.Male,
                    Happiness = TankLivestockHappiness.Content,
                    Healthy = true,
                    LastFed = DateTime.UtcNow.AddDays(-1),
                    UntilNextFeed = new TimeSpan(1, 1, 1, 1, 1)
                };
                
                // Act
                var result = tankLivestock.ToDto();
                
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