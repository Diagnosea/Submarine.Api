using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply.FeedingSupply
{
    public class FeedingSupplyEntity : SupplyEntity
    {
        public FeedingSupplyConsistency Consistency { get; set; }
        public IList<FeedingSupplyNutritionEntity> Nutrition { get; set; }
    }
}