using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedCondition
{
    public class LivestockBreedMineralConditionEntity : LivestockBreedConditionEntity
    {
        public Mineral Mineral { get; set; }

        public LivestockBreedMineralConditionEntity()
        {
            Type = LivestockBreedConditionType.Compositional;
        }
    }
}