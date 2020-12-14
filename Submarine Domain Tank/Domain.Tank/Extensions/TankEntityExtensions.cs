using System.Linq;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankEntityExtensions
    {
        public static TankDto ToDto(this TankEntity tank)
        {
            return new TankDto
            {
                Id = tank.Id,
                Name = tank.Name,
                Water = tank.Water?.ToDto(),
                Livestock = tank.Livestock?
                    .Select(tankLivestock => tankLivestock.ToDto())
                    .ToList(),
                Supplies = tank.Supplies?
                    .Select(tankSupply => tankSupply.ToDto())
                    .ToList()
            };
        }
    }
}