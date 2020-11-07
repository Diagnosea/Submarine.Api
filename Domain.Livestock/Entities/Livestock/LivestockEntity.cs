using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.Livestock
{
    public class LivestockEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LivestockBreedId { get; set; }
        public string Name { get; set; }
        public LivestockSex Sex { get; set; }
        public DateTime Homed { get; set; }
        public LivestockAcclimatisationEntity Acclimatisation { get; set; }
        public IList<LivestockAilmentEntity> Ailments { get; set; }
        public IList<LivestockFeedEntity> Feeds { get; set; }
    }
}