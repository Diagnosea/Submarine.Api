using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankSupplyResponseExtension
    {
        public static TankSupplyResponse ToResponse(this TankSupplyListDto tankSupplyList)
        {
            return new TankSupplyResponse
            {
                SupplyId = tankSupplyList.SupplyId,
                Name = tankSupplyList.Name,
                Component = tankSupplyList.Component,
                Metric = tankSupplyList.Metric,
                Quantity = tankSupplyList.Quantity
            };
        }
    }
}