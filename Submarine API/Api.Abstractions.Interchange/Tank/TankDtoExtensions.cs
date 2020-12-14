using System.Linq;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankDtoExtensions
    {
        public static TankResponse ToResponse(this TankDto tank)
        {
            return new TankResponse
            {
                Id = tank.Id,
                Name = tank.Name,
                Water = tank.Water?.ToResponse(),
                Livestock = tank.Livestock?
                    .Select(tankLivestock => tankLivestock.ToResponse())
                    .ToList(),
                Supplies = tank.Supplies?
                    .Select(tankSupply => tankSupply.ToResponse())
                    .ToList()
            };
        }
    }
}