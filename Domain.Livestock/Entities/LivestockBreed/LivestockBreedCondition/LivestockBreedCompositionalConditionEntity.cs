using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedCondition
{
    public class LivestockBreedCompositionalConditionEntity : LivestockBreedConditionEntity
    {
        public Composite Composite { get; set; }

        public LivestockBreedCompositionalConditionEntity()
        {
            Type = LivestockBreedConditionType.Compositional;
        }
    }
}