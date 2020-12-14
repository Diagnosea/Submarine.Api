using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankLivestockDtoExtensions
    {
        public static TankLivestockResponse ToResponse(this TankLivestockListDto tankLivestockList)
        {
            return new TankLivestockResponse
            {
                LivestockId = tankLivestockList.LivestockId,
                Name = tankLivestockList.Name,
                Sex = tankLivestockList.Sex,
                Happiness = tankLivestockList.Happiness,
                Healthy = tankLivestockList.Healthy,
                LastFed = tankLivestockList.LastFed,
                UntilNextFeed = tankLivestockList.UntilNextFeed
            };
        }
    }
}