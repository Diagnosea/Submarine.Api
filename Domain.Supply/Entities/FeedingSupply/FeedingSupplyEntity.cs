using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities.FeedingSupply
{
    public class FeedingSupplyEntity : SupplyEntity
    {
        public DateTime Expiration { get; set; }
        public FeedingSupplyConsistency Consistency { get; set; }
        public IList<FeedingSupplyNutritionEntity> Nutrition { get; set; }

        public FeedingSupplyEntity()
        {
            Category = SupplyCategory.Feeding;
        }
    }
}