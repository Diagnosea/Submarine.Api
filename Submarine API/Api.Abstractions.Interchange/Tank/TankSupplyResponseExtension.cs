using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankSupplyResponseExtension
    {
        public static TankSupplyResponse ToResponse(this TankSupplyDto tankSupply)
        {
            return new TankSupplyResponse
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