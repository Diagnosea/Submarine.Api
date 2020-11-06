using Diagnosea.Submarine.Abstractions.Enums;

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