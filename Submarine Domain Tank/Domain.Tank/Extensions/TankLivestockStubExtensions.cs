using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankLivestockStubExtensions
    {
        public static TankLivestockListDto ToDto(this TankLivestockStub tankLivestock)
        {
            return new TankLivestockListDto
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