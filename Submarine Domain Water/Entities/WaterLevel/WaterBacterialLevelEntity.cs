using Diagnosea.Submarine.Abstractions.Enums;

namespace Domain.Water.Entities.WaterLevel
{
    public class WaterBacterialLevelEntity : WaterLevelEntity
    {
        public Bacteria Bacteria { get; set; }
    }
}