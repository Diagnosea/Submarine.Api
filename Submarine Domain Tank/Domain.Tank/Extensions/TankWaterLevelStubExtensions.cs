using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankWaterLevelStubExtensions
    {
        public static TankWaterLevelListDto ToDto(this TankWaterLevelStub tankWaterLevel)
        {
            return new TankWaterLevelListDto
            {
                Metric = tankWaterLevel.Metric,
                Quantity = tankWaterLevel.Quantity
            };
        }
    }
}