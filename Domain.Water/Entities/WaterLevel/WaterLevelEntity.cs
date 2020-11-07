using Diagnosea.Submarine.Abstractions.Enums;

namespace Domain.Water.Entities.WaterLevel
{
    public class WaterLevelEntity
    {
        public Metric Metric { get; set; }
        public int Quantity { get; set; }
    }
}