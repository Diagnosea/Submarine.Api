using System;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Abstractions.Enums.Tank;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankLivestockListDto
    {
        public Guid LivestockId { get; set; }
        public string Name { get; set; }
        public LivestockSex Sex { get; set; }
        public TankLivestockHappiness Happiness { get; set; }
        public bool Healthy { get; set; }
        public DateTime? LastFed { get; set; }
        public TimeSpan? UntilNextFeed { get; set; }
    }
}