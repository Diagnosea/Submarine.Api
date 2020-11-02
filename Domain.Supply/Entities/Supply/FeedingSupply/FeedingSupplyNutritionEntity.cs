using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply.FeedingSupply
{
    public class FeedingSupplyNutritionEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public Measurement Measurement { get; set; }
    }
}