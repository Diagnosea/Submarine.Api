using Diagnosea.Submarine.Abstractions.Enums;

namespace Domain.Water.Entities.WaterLevel
{
    public class WaterMineralLevelEntity : WaterLevelEntity
    {
        public Mineral Mineral { get; set; }
    }
}