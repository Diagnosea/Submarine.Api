using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankLivestockDtoExtensions
    {
        public static TankLivestockResponse ToResponse(this TankLivestockDto tankLivestock)
        {
            return new TankLivestockResponse
            {
                LivestockId = tankLivestock.LivestockId,
                Name = tankLivestock.Name,
                Sex = tankLivestock.Sex,
                Happiness = tankLivestock.Happiness,
                Healthy = tankLivestock.Healthy,
                LastFed = tankLivestock.LastFed,
                UntilNextFeed = tankLivestock.UntilNextFeed
            };
        }
    }
}