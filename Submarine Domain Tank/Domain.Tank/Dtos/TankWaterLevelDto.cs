using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankWaterLevelDto
    {
        public Metric Metric { get; set; }
        public int Quantity { get; set; }
    }
}