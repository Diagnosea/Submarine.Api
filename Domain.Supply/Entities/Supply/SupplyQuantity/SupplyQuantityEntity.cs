using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply.SupplyQuantity
{
    public class SupplyQuantityEntity
    {
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}