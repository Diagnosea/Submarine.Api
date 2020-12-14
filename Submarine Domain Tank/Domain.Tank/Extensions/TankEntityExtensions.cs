using System.Linq;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankEntityExtensions
    {
        public static TankListDto ToDto(this TankEntity tank)
        {
            return new TankListDto
            {
                Id = tank.Id,
                Name = tank.Name,
                WaterList = tank.Water?.ToDto(),
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