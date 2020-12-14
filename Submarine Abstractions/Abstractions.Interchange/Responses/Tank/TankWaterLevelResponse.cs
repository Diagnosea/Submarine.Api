using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank
{
    public class TankWaterLevelResponse
    {
        public Metric Metric { get; set; }
        public int Quantity { get; set; }
    }
}