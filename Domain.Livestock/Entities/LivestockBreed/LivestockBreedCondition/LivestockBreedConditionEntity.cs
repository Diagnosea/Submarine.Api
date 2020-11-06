using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedCondition
{
    public class LivestockBreedConditionEntity
    {
        public LivestockBreedConditionType Type { get; set; }
        public Metric Metric { get; set; }
        public int Requirement { get; set; }
        public int Tolerance { get; set; }
    }
}