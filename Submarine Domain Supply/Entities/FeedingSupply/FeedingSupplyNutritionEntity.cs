using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.FeedingSupply
{
    public class FeedingSupplyNutritionEntity
    {
        public string Name { get; set; }
        public int Measurement { get; set; }
        public Metric Metric { get; set; }
    }
}