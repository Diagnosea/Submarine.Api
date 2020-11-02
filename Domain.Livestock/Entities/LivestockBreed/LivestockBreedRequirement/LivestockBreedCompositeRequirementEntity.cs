using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedRequirement
{
    public class LivestockBreedCompositeRequirementEntity : LivestockBreedRequirementEntity
    {
        public Composite Composite { get; set; }
        public int Measurement { get; set; }
        public Metric Metric { get; set; }

        public LivestockBreedCompositeRequirementEntity()
        {
            Type = LivestockBreedRequirementType.Composite,
        }
    }
}