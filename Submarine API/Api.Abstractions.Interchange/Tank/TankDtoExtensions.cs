using System.Linq;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankDtoExtensions
    {
        public static TankResponse ToResponse(this TankListDto tankList)
        {
            return new TankResponse
            {
                Id = tankList.Id,
                Name = tankList.Name,
                Water = tankList.WaterList?.ToResponse(),
                Livestock = tankList.Livestock?
                    .Select(tankLivestock => tankLivestock.ToResponse())
                    .ToList(),
                Supplies = tankList.Supplies?
                    .Select(tankSupply => tankSupply.ToResponse())
                    .ToList()
            };
        }
    }
}