using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedCondition;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed
{
    public class LivestockBreedEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LivestockBreedType Type { get; set; }
        public IList<LivestockBreedConditionEntity> Conditions { get; set; }
    }
}