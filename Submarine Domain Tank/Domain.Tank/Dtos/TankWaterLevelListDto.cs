using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankWaterLevelListDto
    {
        public Metric Metric { get; set; }
        public int Quantity { get; set; }
    }
}