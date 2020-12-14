using System.Linq;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankWaterDtoExtension
    {
        public static TankWaterResponse ToResponse(this TankWaterDto tankWater)
        {
            return new TankWaterResponse
            {
                WaterId = tankWater.WaterId,
                Stage = tankWater.Stage,
                Levels = tankWater.Levels?
                    .Select(tankWaterLevel => tankWaterLevel.ToResponse())
                    .ToList()
            };
        }
    }
}