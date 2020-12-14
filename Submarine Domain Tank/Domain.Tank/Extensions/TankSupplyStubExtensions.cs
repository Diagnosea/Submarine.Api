using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities.TankSupply;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankSupplyStubExtensions
    {
        public static TankSupplyListDto ToDto(this TankSupplyStub tankSupply)
        {
            return new TankSupplyListDto
            {
                SupplyId = tankSupply.SupplyId,
                Name = tankSupply.Name,
                Component = tankSupply.Component,
                Metric = tankSupply.Metric,
                Quantity = tankSupply.Quantity
            };
        }
    }
}