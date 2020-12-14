using System.Linq;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankWaterDtoExtension
    {
        public static TankWaterResponse ToResponse(this TankWaterListDto tankWaterList)
        {
            return new TankWaterResponse
            {
                WaterId = tankWaterList.WaterId,
                Stage = tankWaterList.Stage,
                Levels = tankWaterList.Levels?
                    .Select(tankWaterLevel => tankWaterLevel.ToResponse())
                    .ToList()
            };
        }
    }
}