using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankWaterLevelStubExtensions
    {
        public static TankWaterLevelDto ToDto(this TankWaterLevelStub tankWaterLevel)
        {
            return new TankWaterLevelDto
            {
                Metric = tankWaterLevel.Metric,
                Quantity = tankWaterLevel.Quantity
            };
        }
    }
}