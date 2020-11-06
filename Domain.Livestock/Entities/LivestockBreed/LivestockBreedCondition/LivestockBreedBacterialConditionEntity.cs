using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedCondition
{
    public class LivestockBreedBacterialConditionEntity : LivestockBreedConditionEntity
    {
        public Bacteria Bacteria { get; set; }

        public LivestockBreedBacterialConditionEntity()
        {
            Type = LivestockBreedConditionType.Bacterial;
        }
    }
}