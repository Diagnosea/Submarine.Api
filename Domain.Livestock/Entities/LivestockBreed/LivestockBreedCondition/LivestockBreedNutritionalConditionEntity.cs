using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedCondition
{
    public class LivestockBreedNutritionalConditionEntity : LivestockBreedConditionEntity
    {
        public Nutrition Nutrition { get; set; }

        public LivestockBreedNutritionalConditionEntity()
        {
            Type = LivestockBreedConditionType.Nutritional;
        }
    }
}