using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

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